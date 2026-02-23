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
				
				private void ApplyHorizontalDrag(double airDensity, double delta)
				{
					
				}
				
				private void ApplyVerticalDrag(double airDensity, double delta)
				{
					
				}
				
				public void ApplyDrag(double airDensity, double delta)
				{
					ApplyHorizontalDrag(airDensity, delta);
					ApplyVerticalDrag(airDensity, delta);
				}
				
				public VelocityProperties(Vector2 direction, double muzzleVelocity, double elevationAngle)
				{
					Horizontal = direction * (float)(muzzleVelocity * Math.Cos(elevationAngle));
					Vertical = muzzleVelocity * Math.Sin(elevationAngle);
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
				Vector2 direction = (_targetPos - _device.Position).Normalized();
				double elevation = Battle.Physics.ToRadians(_device.Properties.Angle.Value);
				Altitude = new AltitudeProperties(_device.Properties.Altitude, _device.Properties.Altitude);
				Position = _device.Position;
				Velocity = new VelocityProperties(direction, _device.Properties.MuzzleVelocity, elevation);
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
