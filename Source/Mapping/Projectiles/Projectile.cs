using Godot;
using System;
using System.Linq;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Mapping.Entities;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Projectiles;

public partial class Projectile : Node2D
{
	[Signal] public delegate void DetonatedEventHandler(Device device);

	private Vector2 HorizontalVelocity;
	private double VerticalVelocity;
	private double Altitude = 0;
	
	private double _projectileArea; /* m^2 */
	private Vector2 _direction;
	private double _angleRad;
	
	private double _calculatedLethalBlast = 0;
	private double _calculatedCasualityBlast = 0;
	
	private double _calculatedLethalRadius = 0;
	private double _calculatedCasualityRadius = 0;
	
	private Vector2I _cellPosition;
	private double _cellHeight;
	private bool _justFired = false;
	
	private Device _device;
	private bool _flying = false;
	private bool _exploded = false;
	private DevicePropertiesData _props;
	private Map _map;
	
	[Export] private Sprite2D _explosion;
	[Export] private Sprite2D _shell;
	
	private double _minAlt = 0;
	private double _peakAlt = 0;
	private double _minSize = 1.0;
	private double _peakSize = 1.0;
	
	public override void _PhysicsProcess(double delta)
	{
		if (_flying)
		{
			var airDensity = Calculations.GetAirDensity(Altitude);
			ApplyHorizontalDrag(airDensity, delta);
			ApplyVerticalDrag(airDensity, delta);
			Altitude += VerticalVelocity * (float)delta;
			Position += (HorizontalVelocity * (float)delta) / _map.CellCoefficient;
			
			if (Altitude > _peakAlt)
			{
				_peakAlt = Altitude;
				Scale += new Vector2(.01f, .01f);
			} else
			{
				Scale -= new Vector2(.01f, .01f);
			}
			
			var newCellPos = _map.GroundLayer.LocalToMap(Position);
			if (_map.IsEmpty(newCellPos))
			{
				Disappear();
			}
			
			if (_cellPosition != newCellPos)
			{
				_cellPosition = newCellPos;
				_cellHeight = _map.GetTotalTileHeight(_cellPosition);
			}
			
			if (!_justFired)
			{
				/* fight off edgecase */
				_justFired = true;
			} else
			{
				if (Altitude <= _cellHeight)
				{
					Detonate();
				}
			}
		}
	}
	
	private void Detonate()
	{
		_flying = false;
		
		/* tnt */
		double tntEquivalent = _props.ExplosiveFill * 1.33;
		double casingWeight =  _props.TotalWeight - _props.ExplosiveFill;
		
		/* blast */
		double cubeRootw = Mathf.Pow(tntEquivalent, 1.0/3.0);
		_calculatedLethalBlast = 4.0 * cubeRootw;
		_calculatedCasualityBlast = 9.0 * cubeRootw;
		
		/* frags */
		double lethalFrag = 2.5 * Mathf.Pow(casingWeight * tntEquivalent, 1.0/3.0);
		double casualityFrag = 5.0 * Mathf.Pow(casingWeight * tntEquivalent, 1.0/3.0);
		_calculatedLethalRadius = Mathf.Max(_calculatedLethalBlast, lethalFrag);
		_calculatedCasualityRadius = Mathf.Max(_calculatedCasualityBlast, casualityFrag);
		ApplyDamage();
		AnimateAndDisappear();
	}
	
	public override void _Draw()
	{
		if (_exploded)
		{
			DrawCircle(
				Vector2.Zero, 
				(float)_calculatedCasualityRadius / _map.CellCoefficient.X,
				new Color(0.0f, 0.0f, 0.0f, 0.66f)
			);
		}
	}
	
	private void AnimateAndDisappear()
	{
		double newSize = _calculatedLethalRadius / (_explosion.Texture.GetWidth() / 2);
		_explosion.Scale = new Vector2((float)newSize, (float)newSize);
		_explosion.Visible = true;
		_shell.Visible = false;
		_exploded = true;
		QueueRedraw();
		SceneTreeTimer delayToFire = GetTree().CreateTimer(.5f, false);
		delayToFire.Connect(SceneTreeTimer.SignalName.Timeout , Callable.From(() => {
			Disappear();
		}));
	}
	
	private void ApplyDamage()
	{
		/* to pixels */
		double maxRadiusM = Math.Max(_calculatedCasualityBlast, _calculatedCasualityRadius);
		double px = maxRadiusM / _map.CellCoefficient.X;
		
		/* space */
		var spaceState = GetWorld2D().DirectSpaceState;
		var query = new PhysicsShapeQueryParameters2D();
		var shape = new CircleShape2D();
		shape.Radius = (float)px;
		query.Shape = shape;
		query.CollideWithAreas = true;
		query.CollideWithBodies = false;
		query.Transform = GlobalTransform;
		var results = spaceState.IntersectShape(query, 32);
		if (results.Count > 0)
		{
			var entities = results
				.Where(r => (Node2D)r["collider"] is Entity)
				.Select(r => (Node2D)r["collider"] as Entity);
				
			foreach (var entity in entities)
			{
				var damage = CalculateDamage(entity);
				entity.DecreaseIntegrity(damage);
			}
		}
	}
	
	private double CalculateDamage(Entity entity)
	{
		/* props */
		var tPos = entity.GlobalPosition;
		double tAltitude = _cellHeight;
		double toughness = 1.0 - entity.Properties.Toughness;
		
		/* distance & diffs */
		double distancePx = GlobalPosition.DistanceTo(tPos);// / _map.CellCoefficient.X;
		double altDiff = Math.Abs(tAltitude - Altitude);
		double distance3d = Math.Sqrt(Math.Pow(distancePx, 2.0) + Math.Pow(altDiff, 2.0));
		
		/* damage */
		double blastDmg = CalculateBlastDamage(distance3d, altDiff, tAltitude);
		double fragDmg = CalculateFragDamage(distance3d, altDiff, tAltitude);
		double rawDamage = Math.Max(blastDmg, fragDmg);
		
		return rawDamage * toughness;
	}
	
	private double CalculateBlastDamage(double distance3d, double altDiff, double tAltitude)
	{
		if (distance3d > _calculatedCasualityBlast / _map.CellCoefficient.X) return 0.0;
		double overpressure = CalculateOverpressure(distance3d);
		double baseDamage = overpressure * 100.0;
		double falloff = CalculateFalloff(distance3d);
		double alt = CalculateAltitude(altDiff, tAltitude);
		
		return baseDamage * falloff * alt;
	}
	
	private double CalculateFragDamage(double distance3d, double altDiff, double tAltitude)
	{
		if (distance3d > _calculatedCasualityRadius / _map.CellCoefficient.X) return 0.0;
		double casingWeight =  _props.TotalWeight - _props.ExplosiveFill;
		double baseDamage = (10.0 * casingWeight * _props.ExplosiveFill) / Math.Max(distance3d, 1.0);
		double falloff = CalculateFalloff(distance3d);
		double alt = CalculateAltitude(altDiff, tAltitude) * 0.8;
		double frags = casingWeight / 5.0;
		return baseDamage * falloff * alt * frags;
	}
	
	private double CalculateOverpressure(double distance)
	{
		var tntEquivalent = _props.ExplosiveFill * 1.33;
		var s = distance / Math.Pow(tntEquivalent, 1/3);
		if (s < .5) return 20.0;
		else if (s < 1.0) return 10.0 / (Math.Pow(s, 2.0));
		else if (s < 3.0) return 1.5 / (Math.Pow(s, 2.0));
		else if (s < 10.0) return 0.4 / (Math.Pow(s, 2.0));
  		return 0.1 / (Math.Pow(s, 2.0));
	}
	
	private double CalculateFalloff(double distance)
	{
		//return Mathf.Clamp(Mathf.Pow(distance / 2, -1.5), 0.01, 1);
		if (distance <= 2.0) return 1.0;
		else if (distance <= 5.0) return .8;
		else if (distance <= 10.0) return .5;
		else if (distance <= 20.0) return .25;
		else if (distance <= 30.0) return .1;
		else if (distance <= 50.0) return .05;
		else return .02;
	}
	
	private double CalculateAltitude(double altDiff, double tAltitude)
	{
		if (altDiff < 1.0) return 1.0;
		
		if (tAltitude > altDiff)
		{
			if (altDiff > 20.0) return .3;
			else if (altDiff > 10.0) return .5;
			else if (altDiff > 5.0) return .7;
			else return .9;
		} else
		{
			if (altDiff > 20.0) return 1.3;
			else if (altDiff > 10.0) return 1.2;
			else if (altDiff > 5.0) return 1.1;
			else return 1.0;
		}
	}
	
	private void Disappear()
	{
		_flying = false;
		EmitSignal(SignalName.Detonated, _device);
		QueueFree();
	}

	private void ApplyHorizontalDrag(double airDensity, double delta)
	{
		double horizontalSpeed = HorizontalVelocity.Length();

		if (horizontalSpeed < 0.1)
		{
			return;
		} else
		{
			double dragForce = 
				airDensity * _props.DragCoefficient
				* _projectileArea * Math.Pow(horizontalSpeed, 2) * 0.5;
			double dragAcceleration = dragForce / _props.TotalWeight;
			var horizontalVelocityDelta = HorizontalVelocity.Normalized() * (float)(dragAcceleration * delta);
			HorizontalVelocity -= horizontalVelocityDelta;
		}
	}
	
	private void ApplyVerticalDrag(double airDensity, double delta)
	{
		double verticalSpeed = Math.Abs(VerticalVelocity);
		double dragForce = airDensity * _props.DragCoefficient * 
		_projectileArea * Math.Pow(verticalSpeed, 2) * 0.5;
		double dragAcceleration = dragForce / _props.TotalWeight;
		double dragDirection = -Math.Sign(VerticalVelocity);
		var verticalVelocityDelta =
			(Constants.Physics.Gravity * -1 + dragDirection * dragAcceleration)
			* delta;
		VerticalVelocity += verticalVelocityDelta;
	}
	
	public void Set(Device device, Map map)
	{
		_device = device;
		Position = _device.Position;
		_props = (DevicePropertiesData)_device.Properties;
		_map = map;
		
		var deviceCellPos = _map.EntityLayer.LocalToMap(_device.Position);
		GroundTile groundTile = (GroundTile)_map.GroundLayer.Flash(deviceCellPos)[0];

		Altitude = groundTile.GetFullHeight() + device.Properties.Height;
		_minAlt = Altitude;
		
		var radius = (_props.Caliber / 1000.0) / 2.0;
		var traverse = _device.Azimuth + _device.Traverse;
		_projectileArea = Mathf.Pi * Mathf.Pow(radius, 2);
		_direction = Calculations.AzimuthToDirection(traverse);
		_angleRad = Calculations.ToRadians(_device.Angle);
		_cellPosition = deviceCellPos;
		_cellHeight = _map.GetTotalTileHeight(deviceCellPos);
		Rotation = _direction.Angle();

		var initHorizontalVelocity = _direction * (float)(_props.MuzzleVelocity * Math.Cos(_angleRad));
		var initVerticalVelocity = _props.MuzzleVelocity * Math.Cos(_angleRad);
		HorizontalVelocity = initHorizontalVelocity;
		VerticalVelocity = initVerticalVelocity;
		 _justFired = false;
	}
	
	public void Launch()
	{
		_device.ExpendShell();
		_flying = true;
	}
}
