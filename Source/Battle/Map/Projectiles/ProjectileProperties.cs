using Godot;
using System;

namespace Drumstalotajs.Battle.Map.Projectiles
{
	public partial class Projectile : Node2D
	{
		public class ProjectileProperties
		{
			public class AltitudeProperties
			{
				public double Start { get; private set; }
				public double End { get; private set; }
				public double Value { get; set; }
				
				public AltitudeProperties (double start, double end)
				{
					Start = start;
					Value = start;
					End = end;
				}
			}
			
			public class VelocityProperties
			{
				public Vector2 Horizontal { get; private set; }
				public double Vertical { get; private set; }
				
				private Entities.Device.DeviceProjectile _projectile; 
				
				private void ApplyHorizontalDrag(double airDensity, double delta)
				{
					double horizontalSpeed = Horizontal.Length();
					
					if (horizontalSpeed < 0.1)
					{
						return;
					} else
					{
						double dragForce = airDensity * _projectile.DragCoefficient * _projectile.Area * Math.Pow(horizontalSpeed, 2) * 0.5;
						double dragAcceleration = dragForce / _projectile.TotalWeight;
						Horizontal -= Horizontal.Normalized() * (float)(dragAcceleration * delta);
					}
				}
				
				private void ApplyVerticalForce(double airDensity, double delta)
				{
					double verticalSpeed = Math.Abs(Vertical);
					double dragForce = airDensity * _projectile.DragCoefficient * _projectile.Area * Math.Pow(verticalSpeed, 2) * 0.5;
					double dragAcceleration = dragForce / _projectile.TotalWeight;
					double dragDirection = -Math.Sign(Vertical);
					Vertical += (Battle.Physics.Gravity * -1.0 + dragDirection * dragAcceleration) * delta;
				}
				
				public void ApplyDrag(double airDensity, double delta)
				{
					ApplyHorizontalDrag(airDensity, delta);
					ApplyVerticalForce(airDensity, delta);
				}
				
				public VelocityProperties(Entities.Device.DeviceProjectile projectile, Vector2 direction, double muzzleVelocity, double elevationAngle)
				{
					Horizontal = direction * (float)(muzzleVelocity * Math.Cos(elevationAngle));
					Vertical = muzzleVelocity * Math.Sin(elevationAngle);
					_projectile = projectile;
				}
			}
			
			public AltitudeProperties Altitude { get; private set; }
			public VelocityProperties Velocity { get; private set; }
			public Vector2 Position { get; private set; }
			
			private Entities.Device _device;
			private Vector2 _targetPos;
			
			public void NextStep(double delta)
			{
				double airDensity = Battle.Physics.CalculateAirDensity(Altitude.Value);
				Velocity.ApplyDrag(airDensity, delta);
				Altitude.Value += Velocity.Vertical * (float)delta;
				Position += Velocity.Horizontal * (float)delta;
			}
			
			public void Reset()
			{
				Vector2 direction = Topography.AzimuthToDirection(_device.Properties.Traverse.Value);
				double elevation = Physics.ToRadians(_device.Properties.Angle.Value);
				Altitude = new AltitudeProperties(_device.Properties.Altitude, _device.Properties.Altitude);
				Position = _device.Position;
				Velocity = new VelocityProperties(_device.Projectile, direction, _device.Properties.MuzzleVelocity, elevation);
			}
			
			public ProjectileProperties(Entities.Device device)
			{
				_targetPos = new Vector2(100, 100);
				_device = device;
				Reset();
			}
		}
	}
}
