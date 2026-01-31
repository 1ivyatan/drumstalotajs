using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Device : Entity
	{
		private TileMapLayer _parent;
		private Sprite2D _sprite;
		
		public sealed override void _Ready()
		{
			_parent = GetParent<TileMapLayer>();
			_sprite = GetNode<Sprite2D>("Sprite");
			
			if (EntityResource != null)
			{
				_sprite.Texture = EntityResource.Sprites[0];
			}
		}
	}
}
