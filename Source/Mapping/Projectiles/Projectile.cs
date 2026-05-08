using Godot;
using System;
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
	[Signal] public delegate void DetonatedEventHandler();
	
	private Vector2 MapPosition;
	private Vector2 HorizontalVelocity;
	private double VerticalVelocity;
	private double Altitude = 0;
	
	private double _projectileArea; /* m^2 */
	private Vector2 _direction;
	private double _angleRad;
	private Vector2I _cellPosition;
	
	private Device _device;
	private bool _flying = false;
	private DevicePropertiesData _props;
	private Map _map;
	
	public override void _PhysicsProcess(double delta)
	{
		if (_flying)
		{
			var airDensity = Calculations.GetAirDensity(Altitude);
			ApplyHorizontalDrag(airDensity, delta);
			ApplyVerticalDrag(airDensity, delta);
			Altitude += VerticalVelocity * (float)delta;
			Position += (HorizontalVelocity * (float)delta) / _map.CellCoefficient;
			Rotation = _direction.Angle();
			GD.Print(Altitude);
		}
	}
	/*
	
	public override void _Ready()
	{
	}
	Constants
{
	public static class Vector2I
	{
		public static Godot.Vector2I Negative => new Godot.Vector2I(-1, -1);
	}
	
	public static class Mapping
	{
		public static Godot.Vector2I TileSize => new Godot.Vector2I(32, 32);
		public static int MaxProjectileCount => 50;
	}
	
	public static class Physics
	{
	
	*/
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
		
		MapPosition = Position;
		Altitude = groundTile.GetFullHeight();
		
		var radius = (_props.Caliber / 1000.0) / 2.0;
		var traverse = _device.Azimuth + _device.Traverse;
		_projectileArea = Mathf.Pi * Mathf.Pow(radius, 2);
		_direction = Calculations.AzimuthToDirection(traverse);
		_angleRad = Calculations.ToRadians(_device.Angle);
		_cellPosition = deviceCellPos;

		var initHorizontalVelocity = _direction * (float)(_props.MuzzleVelocity * Math.Cos(_angleRad));
		var initVerticalVelocity = _props.MuzzleVelocity * Math.Cos(_angleRad);
		HorizontalVelocity = initHorizontalVelocity;
		VerticalVelocity = initVerticalVelocity;
	}
	
	public void Launch()
	{
		_device.ExpendShell();
		_flying = true;
	}
}
