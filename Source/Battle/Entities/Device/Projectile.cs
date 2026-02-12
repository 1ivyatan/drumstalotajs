using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Projectile : Area2D
	{
		public class ProjectileMotionProperties
		{
			public double Angle { get; private set; }
			public double Range { get; private set; }
			public double InitialVelocity { get; private set; }
			public double InitialHeight { get; private set; }
			public double Time { get; private set; }
			public double BaseHeight { get; private set; }
			private double _vy0;
		
			public double GetHeight(double time)
			{
				return InitialHeight + (_vy0 * time - 0.5 * Battle.Physics.Gravity * Math.Pow(time, 2));
			}
			
			public ProjectileMotionProperties(Entities.Device device, Drumstalotajs.Resources.Level levelData, double relHeight)
			{
				Angle = device.Angle.Value;
				InitialVelocity = device.DeviceResource.MuzzleVelocity;
				BaseHeight = levelData.BaseHeight;
				InitialHeight =  relHeight + BaseHeight;
				Range = (
					Math.Pow(InitialVelocity, 2) * Math.Sin(2 * Battle.Physics.ToRadians(Angle))
				) / Battle.Physics.Gravity;
				_vy0 = InitialVelocity * Math.Sin(Battle.Physics.ToRadians(Angle));
//				Time = (_vy0 + Math.Sqrt( Math.Pow(_vy0, 2) + 2 * Battle.Physics.Gravity * InitialHeight) / Battle.Physics.Gravity);
				Time = (2 * InitialVelocity * Math.Sin(Battle.Physics.ToRadians(Angle)) ) / Battle.Physics.Gravity;
			}
		}
		
		public class MapMotionProperties
		{
			public double Rotation { get; private set; }
			public Vector2 StartPosition { get; private set; }
			public Vector2 EndPosition { get; private set; }
			public Vector2 Range { get; private set; }
			
			public MapMotionProperties(Entities.Device device, ProjectileMotionProperties projectileMotion, Drumstalotajs.Resources.Level levelData)
			{
				Rotation = (90.0 - device.Traverse.Azimuth) * (Math.PI / 180.0);
				StartPosition = device.Position;
				this.EndPosition = new Vector2(
					(float)(StartPosition.X + ((projectileMotion.Range * Physics.Pixels * 1) * Math.Cos(Rotation)) / levelData.Scale),
					(float)(StartPosition.Y + ((projectileMotion.Range * Physics.Pixels * -1) * Math.Sin(Rotation))/ levelData.Scale)
				);
				this.Range = this.EndPosition - this.StartPosition;
			}
		}
		
		[Signal]
		public delegate void LandedEventHandler();
		
		public ProjectileMotionProperties ProjectileMotion { get; private set; }
		public MapMotionProperties MapMotion { get; private set; }
		private Tween _tween = null;
		
		private Map.GroundLayer _groundLayer;
		private Map.EntityLayer _entityLayer;
		
		public double GetTrajectoryHeight()
		{
			Vector2 distance = Position - MapMotion.StartPosition;
			double percentage = Math.Sqrt(Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2)) / Math.Sqrt(Math.Pow(MapMotion.Range.X, 2) + Math.Pow(MapMotion.Range.Y, 2));
			return ProjectileMotion.GetHeight(percentage * ProjectileMotion.Time);
		}
		
		public void SetMotion(Entities.Device device, Drumstalotajs.Resources.Level levelData, double relHeight)
		{	
			ProjectileMotion = new ProjectileMotionProperties(device, levelData, relHeight);
			MapMotion = new MapMotionProperties(device, ProjectileMotion, levelData);
			Position = MapMotion.StartPosition;
		}
		
		private void Fire()
		{
			_tween = CreateTween();
			_tween.SetProcessMode(0);
			_tween.SetTrans((Tween.TransitionType)1);
			_tween.TweenProperty(this, "position", MapMotion.EndPosition, ProjectileMotion.Time);
			_tween.TweenCallback(Callable.From(Destroy));
		}
		
		private void Destroy()
		{
			if (_tween != null && _tween.IsValid())
			{
				_tween.Kill();
			}
			EmitSignal(SignalName.Landed);
			QueueFree();
		}
		
		public override void _PhysicsProcess(double delta)
		{
			if (_tween != null && _tween.IsValid())
			{
				Vector2I gridPosition = _groundLayer.LocalToMap(Position);
				
				if (!_groundLayer.GetCellAtlasCoords(gridPosition).Equals(new Vector2I(-1, -1)))
				{
					double tileHeight = _groundLayer.GetHeight(gridPosition); //(double)data.GetCustomData("RelativeHeight") + ProjectileMotion.BaseHeight;
					double projectileHeight = GetTrajectoryHeight();
					double objectHeight = 0;
					
					Entities.Type entityType = _entityLayer.GetEntityType(gridPosition);
					Entities.Defense defenseEntity = null;
					if (entityType == Entities.Type.Defense)
					{
						defenseEntity = (_entityLayer.EntityPointers[entityType][gridPosition] as Entities.Defense);
						objectHeight += defenseEntity.DefenseResource.Height;
					}
				
					if (projectileHeight < tileHeight + objectHeight)
					{
						if (defenseEntity != null) defenseEntity.Destroy();
						Destroy();
					}
				} else
				{
					Destroy();
				}
				
			}
		}
		
		public override void _Ready()
		{
			_entityLayer = GetNode<TileMapLayer>("..") as Map.EntityLayer;
			_groundLayer = GetNode<TileMapLayer>("../../GroundLayer") as Map.GroundLayer;
			Fire();
		}
	}
}
