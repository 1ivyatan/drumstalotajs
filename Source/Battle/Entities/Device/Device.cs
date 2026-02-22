using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Device : Entity
	{
		public Resources.Entities.Device DeviceResource => EntityResource as Resources.Entities.Device;
		public DeviceProperties Properties { get; private set; }
		public DeviceProjectile Projectile { get; private set; }
		
		private TileMapLayer _parent;
		private Map.Layers.GroundLayer _groundLayer;
		private Sprite2D _sprite;
		
		public Projectile Fire()
		{
			Entities.Projectile projectile = ResourceLoader.Load<PackedScene>("res://Scenes/Battle/Entities/Devices/Projectile.tscn").Instantiate() as Projectile;
			Resources.Levels.Level levelData = (GetNode("../../../..") as Battle.Scene).Level;
			Vector2I gridPosition = _groundLayer.LocalToMap(Position);
			TileData tileData = _groundLayer.GetCellTileData(gridPosition);
			projectile.SetProjectile(Properties, Projectile, levelData, tileData);
			//projectile.SetProjectile(this, levelData, tileData);
			_parent.AddChild(projectile);
			return projectile;
		}
		
		public override void _Ready()
		{
			_parent = GetParent<TileMapLayer>();
			_groundLayer = _parent.GetNode<TileMapLayer>("../GroundLayer") as Map.Layers.GroundLayer;
			_sprite = GetNode<Sprite2D>("Sprite");
			
			Vector2I gridPosition = _groundLayer.LocalToMap(Position);
			TileData tileData = _groundLayer.GetCellTileData(gridPosition);
			
			if (EntityResource != null)
			{
				Properties = new DeviceProperties(this, _groundLayer, tileData);
				Projectile = new DeviceProjectile(Properties, DeviceResource.Projectiles[0]); ///
				_sprite.Texture = EntityResource.Sprites[0];
			}
		}
	}
}
