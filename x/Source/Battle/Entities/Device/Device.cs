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
		
		public override void _Ready()
		{
			_parent = GetParent<TileMapLayer>();
			
			if (EntityResource != null)
			{
				_groundLayer = _parent.GetNode<TileMapLayer>("../GroundLayer") as Map.Layers.GroundLayer;
				_sprite = GetNode<Sprite2D>("Sprite");
				Vector2I gridPosition = _groundLayer.LocalToMap(Position);
				TileData tileData = _groundLayer.GetCellTileData(gridPosition);
				Properties = new DeviceProperties(this, _groundLayer, tileData);
				Projectile = new DeviceProjectile(Properties, DeviceResource.Projectiles[0]); ///
				_sprite.Texture = EntityResource.Sprites[0];
			}
		}
	}
}
