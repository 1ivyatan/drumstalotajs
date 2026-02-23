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
			
			public AltitudeProperties Altitude { get; private set; }
			public Vector2 Position { get; private set; }
			public Vector2 HorizontalVelocity { get; private set; }
			public double VerticalVelocity { get; private set; }
			
			private Entities.Device _device;
			private Vector2 _targetPos;
			
			private void ApplyHorizontalDrag(double airDensity, double delta)
			{
				
			}
			
			private void ApplyVerticalDrag(double airDensity, double delta)
			{
				
			}
			
			public void NextStep(double delta)
			{
				double airDensity = Battle.Physics.CalculateAirDensity(Altitude.Value);
				ApplyHorizontalDrag(airDensity, delta);
				ApplyVerticalDrag(airDensity, delta);
				Altitude.Value += VerticalVelocity * (float)delta;
				Position += HorizontalVelocity * (float)delta;
			}
			
			public void Reset()
			{
				Vector2 direction = (_targetPos - _device.Position).Normalized();
				double elevation = Battle.Physics.ToRadians(_device.Properties.Angle.Value);
				Altitude = new AltitudeProperties(_device.Properties.Altitude, _device.Properties.Altitude);
				Position = _device.Position;
				HorizontalVelocity = direction * (float)(_device.Properties.MuzzleVelocity * Math.Cos(elevation));
				VerticalVelocity = _device.Properties.MuzzleVelocity * Math.Sin(elevation);
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
