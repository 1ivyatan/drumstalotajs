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
			public Vector2 HVelocity { get; private set; }
			public double VVelocity { get; private set; }
			
			private Entities.Device _device;
			private Vector2 _targetPos;
			
			public void NextStep()
			{
				GD.Print("next");
			}
			
			public void Reset()
			{
				Vector2 direction = (_targetPos - _device.Position).Normalized();
				double elevation = Battle.Physics.ToRadians(_device.Properties.Angle.Value);
				Altitude = new AltitudeProperties(_device.Properties.Altitude, _device.Properties.Altitude);
				Position = _device.Position;
				HVelocity = direction * (float)(_device.Properties.MuzzleVelocity * Math.Cos(elevation));
				VVelocity = _device.Properties.MuzzleVelocity * Math.Sin(elevation);
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
