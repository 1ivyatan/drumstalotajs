using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Device : Entity
	{
		public Resources.Entities.Device DeviceResource => EntityResource as Resources.Entities.Device;
		public DeviceProperties Properties { get; private set; }
		public DeviceProjectile[] Projectiles { get; private set; }
		
		private TileMapLayer _parent;
		private Map.GroundLayer _groundLayer;
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
			_groundLayer = _parent.GetNode<TileMapLayer>("../GroundLayer") as Map.GroundLayer;
			_sprite = GetNode<Sprite2D>("Sprite");
			
			if (EntityResource != null)
			{
				Properties = new DeviceProperties(this, _groundLayer);
				_sprite.Texture = EntityResource.Sprites[0];
			}
		}
	}
}
