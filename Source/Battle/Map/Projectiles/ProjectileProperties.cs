using Godot;
using System;

namespace Drumstalotajs.Battle.Map.Projectiles
{
	public partial class Projectile : Node2D
	{
		public class ProjectileProperties
		{	
			public class VelocityProperties
			{
				public Vector2 Horizontal { get; private set; }
				public double Vertical { get; private set; }
				
				private Entities.Device.DeviceProjectile _projectileProperties; 
				
				private void ApplyHorizontalDrag(double airDensity, double delta)
				{
					double horizontalSpeed = Horizontal.Length();
					
					if (horizontalSpeed < 0.1)
					{
						return;
					} else
					{
						double dragForce = airDensity * _projectileProperties.Projectile.DragCoefficient * _projectileProperties.Projectile.Area * Math.Pow(horizontalSpeed, 2) * 0.5;
						double dragAcceleration = dragForce / _projectileProperties.Projectile.TotalWeight;
						Horizontal -= Horizontal.Normalized() * (float)(dragAcceleration * delta);
					}
				}
				
				private void ApplyVerticalForce(double airDensity, double delta)
				{
					double verticalSpeed = Math.Abs(Vertical);
					double dragForce = airDensity * _projectileProperties.Projectile.DragCoefficient * _projectileProperties.Projectile.Area * Math.Pow(verticalSpeed, 2) * 0.5;
					double dragAcceleration = dragForce / _projectileProperties.Projectile.TotalWeight;
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
					_projectileProperties = projectile;
				}
			}
			
			public VelocityProperties Velocity { get; private set; }
			public double Altitude { get; protected set; }
			public Vector2 Position { get; private set; }
			public Entities.Device Device { get; private set; }
			
			public void NextStep(double delta)
			{
				double airDensity = Battle.Physics.CalculateAirDensity(Altitude);
				Velocity.ApplyDrag(airDensity, delta);
				Altitude += Velocity.Vertical * (float)delta;
				Position += Velocity.Horizontal * (float)delta;
			}
			
			public void Reset()
			{
				Vector2 direction = Topography.AzimuthToDirection(Device.Properties.Traverse.Value);
				double elevation = Physics.ToRadians(Device.Properties.Angle.Value);
				Altitude = Device.Properties.Altitude;
				Position = Device.Position;
				Velocity = new VelocityProperties(Device.Projectile, direction, Device.Properties.MuzzleVelocity, elevation);
			}
			
			public ProjectileProperties(Entities.Device device)
			{
				Device = device;
				Reset();
			}
		}
	}
}
