using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Device : Entity
	{
		public Resources.Entities.Device DeviceResource => EntityResource as Resources.Entities.Device;
		public DeviceProperties Properties { get; private set; }
		public DeviceProjectile Projectile { get; private set; }
		public int ProjectileIndex { get; set; }
		
		private TileMapLayer _parent;
		private Map.GroundLayer _groundLayer;
		private Sprite2D _sprite;
		
		public Projectile Fire()
		{
			Projectile projectile = ResourceLoader.Load<PackedScene>("res://Scenes/Battle/Entities/Devices/Projectile.tscn").Instantiate() as Projectile;
			Resources.Levels.Level levelData = (GetNode("../../../..") as Battle.Scene).Level;
			
			Vector2I gridPosition = _groundLayer.LocalToMap(Position);
			TileData tileData = _groundLayer.GetCellTileData(gridPosition);
			//double relTileHeight = (double)data.GetCustomData("RelativeHeight");
			
			projectile.SetProjectile(this, levelData, tileData);
			_parent.AddChild(projectile);
			return projectile;
		}
		
		public override void _Ready()
		{
			_parent = GetParent<TileMapLayer>();
			_groundLayer = _parent.GetNode<TileMapLayer>("../GroundLayer") as Map.GroundLayer;
			_sprite = GetNode<Sprite2D>("Sprite");
			Projectile = new DeviceProjectile(DeviceResource.Projectiles[0]);
			
			if (EntityResource != null)
			{
				Properties = new DeviceProperties(this, _groundLayer);
				_sprite.Texture = EntityResource.Sprites[0];
			}
		}
	}
}
