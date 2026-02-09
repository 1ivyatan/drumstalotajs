using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Projectile : Node2D
	{
		public class ProjectileMotionProperties
		{
			public double Angle { get; private set; }
			public double Range { get; private set; }
			public double InitialVelocity { get; private set; }
			public double Time { get; private set; }
			private double _vy0;
		
			public double GetHeight(double time)
			{
				return _vy0 * time - 0.5 * Battle.Physics.Gravity * Math.Pow(time, 2);
			}
			
			public ProjectileMotionProperties(Entities.Device device)
			{
				Angle = device.Angle.Value;
				InitialVelocity = device.DeviceResource.Velocity;
				Range = (
					Math.Pow(InitialVelocity, 2) * Math.Sin(2 * Battle.Physics.ToRadians(Angle))
				) / Battle.Physics.Gravity;
				Time = (2 * InitialVelocity * Math.Sin(Battle.Physics.ToRadians(Angle)) ) / Battle.Physics.Gravity;
				_vy0 = InitialVelocity * Math.Sin(Battle.Physics.ToRadians(Angle));
			}
		}
		
		public class MapMotionProperties
		{
			public double Rotation { get; private set; }
			public Vector2 StartPosition { get; private set; }
			public Vector2 EndPosition { get; private set; }
			public Vector2 Range { get; private set; }
			
			public MapMotionProperties(Entities.Device device, ProjectileMotionProperties projectileMotion)
			{
				Rotation = (90.0 - device.Traverse.Azimuth) * (Math.PI / 180.0);
				StartPosition = device.Position;
				this.EndPosition = new Vector2(
					(float)(StartPosition.X + ((projectileMotion.Range * Physics.Pixels * 1) * Math.Cos(Rotation)) / 4),
					(float)(StartPosition.Y + ((projectileMotion.Range * Physics.Pixels * -1) * Math.Sin(Rotation))/ 4)
				);
				this.Range = this.EndPosition - this.StartPosition;
			}
		}
		
		[Signal]
		public delegate void LandedEventHandler();
		
		public ProjectileMotionProperties ProjectileMotion { get; private set; }
		public MapMotionProperties MapMotion { get; private set; }
		private Tween tween = null;
		
		public void SetMotion(Entities.Device device)
		{
			ProjectileMotion = new ProjectileMotionProperties(device);
			MapMotion = new MapMotionProperties(device, ProjectileMotion);
			Position = MapMotion.StartPosition;
		}
		
		private void Fire()
		{
			tween = CreateTween();
			tween.SetProcessMode(0);
			tween.SetTrans((Tween.TransitionType)1);
			tween.TweenProperty(this, "position", MapMotion.EndPosition, ProjectileMotion.Time);
			tween.TweenCallback(Callable.From(Destroy));
		}
		
		private void Destroy()
		{
			EmitSignal(SignalName.Landed);
			QueueFree();
		}
		
		public override void _Ready()
		{
			Fire();
		}
	}
}
