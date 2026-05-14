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

		double tntEquivalent = _props.ExplosiveFill * 1.33;
		double casingWeight =  _props.TotalWeight - _props.ExplosiveFill;
		double cubeRootw = Mathf.Pow(tntEquivalent, 1/3);
		
		/* blast */
		_calculatedLethalBlast = 4.0 * cubeRootw;
		_calculatedCasualityBlast = 9.0 * cubeRootw;
		
		/* frags */
		double lethalFrag = 2.5 * Mathf.Pow(casingWeight * tntEquivalent, 1/3);
		double casualityFrag = 5.0 * Mathf.Pow(casingWeight * tntEquivalent, 1/3);
		
		_calculatedLethalRadius = Mathf.Max(_calculatedLethalBlast, lethalFrag);
		_calculatedCasualityRadius = Mathf.Max(_calculatedCasualityBlast, casualityFrag);
		
	//	GD.Print(_calculatedLethalRadius);
	//	GD.Print(_calculatedCasualityRadius);
		
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
				new Color(0.0f, 0.0f, 0.0f, 0.35f)
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
		SceneTreeTimer delayToFire = GetTree().CreateTimer(.5f);
		delayToFire.Connect(SceneTreeTimer.SignalName.Timeout , Callable.From(() => {
			Disappear();
		}));
	}
	
	private void ApplyDamage()
	{
		var spaceState = GetWorld2D().DirectSpaceState;
		var query = new PhysicsShapeQueryParameters2D();
		var shape = new CircleShape2D();
		shape.Radius = (float)(_calculatedCasualityRadius / _map.CellCoefficient.X);
		query.Shape = shape;
		query.CollideWithAreas = true;
		query.Transform = GlobalTransform;
		
		var results = spaceState.IntersectShape(query, 32);
		if (results.Count > 0)
		{
			var entities = results
				.Where(r => (Node2D)r["collider"] is Entity)
				.Select(r => (Node2D)r["collider"] as Entity);
				
			foreach (var entity in entities)
			{
				var distance = GlobalPosition.DistanceTo(entity.GlobalPosition);
				var damage = CalculateDamageAtDistance(entity, distance);
				entity.DecreaseIntegrity(damage);
			}
		}
	}
	
	private double CalculateOverpressure(double distance)
	{
		var tntEquivalent = _props.ExplosiveFill * 1.33;
		var s = distance / Math.Pow(tntEquivalent, 1/3);
		if (s < .5) return 20;
		else if (s < 1) return 10 / (Math.Pow(s, 2));
		else if (s < 3) return 1.5 / (Math.Pow(s, 2));
		else if (s < 10) return 0.4 / (Math.Pow(s, 2));
  		return 0.1 / (Math.Pow(s, 2));
	}
	
	private double CalculateDamageAtDistance(Entity entity, double distance)
	{
		var pxDistance = distance * _map.CellCoefficient.X;
		var shellAltitude = Altitude;
		var targetAltitude = _cellHeight;
		var altitudeDiff = targetAltitude - shellAltitude;
		var trueDistance = Mathf.Sqrt(Mathf.Pow(pxDistance, 2) + Mathf.Pow(altitudeDiff, 2));
		var baseDamage = (_props.ExplosiveFill * 10) / Mathf.Max(trueDistance, 1);
		var distanceFallOff = GetDistanceFalloff(trueDistance);
		var altitudeMod = 1.0;
		if (altitudeDiff > 5) altitudeMod = Mathf.Max(0.3, 1 - (altitudeDiff / 50));
		else if (altitudeDiff < -5) altitudeMod = Mathf.Min(1.3, 1 + (Mathf.Abs(altitudeDiff) / 100));
		var materialMultiplier = 1 - entity.Properties.Toughness;
		
		return baseDamage * distanceFallOff * altitudeMod * materialMultiplier;
	}
	
	private double GetDistanceFalloff(double distance)
	{
		return Mathf.Clamp(Mathf.Pow(distance / 2, -1.5), 0.01, 1);
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
