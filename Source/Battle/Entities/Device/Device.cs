using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Device : Entity
	{
		public class AngleProperties
		{
			public double Min {get; private set;}
			public double Max {get; private set;}
			public double Value
			{
				get;
				set { field = Mathf.Clamp(value, Min, Max); }
			}
			
			public AngleProperties(double startingAngle, double angleRadius)
			{
				Min = startingAngle - angleRadius;
				Max = startingAngle + angleRadius;
				Value = startingAngle;
			}
		}
		
		public class TraverseProperties
		{
			public bool Locked { get; set; }
			public double Azimuth { get; set; }
			public TraverseProperties()
			{
				Azimuth = 0;
				Locked = false;
			}
		}
		
		public AngleProperties Angle { get; private set; }
		public TraverseProperties Traverse { get; private set; }
		
		public Resources.Entities.Device DeviceResource => EntityResource as Resources.Entities.Device;
		public DeviceProperties Properties { get; private set; }
		public ProjectileProperties[] Projectiles { get; private set; }
		
		private TileMapLayer _parent;
		private TileMapLayer _groundLayer;
		private Sprite2D _sprite;
		
		public Projectile Fire()
		{
			Projectile projectile = ResourceLoader.Load<PackedScene>("res://Scenes/Battle/Entities/Devices/Projectile.tscn").Instantiate() as Projectile;
			Drumstalotajs.Resources.Level levelData = (GetNode("../../../..") as Battle.Scene).Level;
			
			Vector2I gridPosition = _groundLayer.LocalToMap(Position);
			TileData data = _groundLayer.GetCellTileData(gridPosition);
			double relTileHeight = (double)data.GetCustomData("RelativeHeight");
			
			projectile.SetMotion(this, levelData, relTileHeight);
			_parent.AddChild(projectile);
			return projectile;
		}
		
		public override void _Ready()
		{
			_parent = GetParent<TileMapLayer>();
			_groundLayer = _parent.GetNode<TileMapLayer>("../GroundLayer");
			_sprite = GetNode<Sprite2D>("Sprite");
			
			Angle = new AngleProperties(DeviceResource.StartingAngle, DeviceResource.AngleRadius);
			Traverse = new TraverseProperties();
			
			if (EntityResource != null)
			{
				_sprite.Texture = EntityResource.Sprites[0];
			}
		}
	}
}
