using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Map.Entities.Markers
{
	public partial class DevicePlacementMarker : Entity
	{
		private Sprite2D sprite;
		
		public override void _Ready()
		{
			sprite = GetNode<Sprite2D>("Sprite");
			
			if (EntityResource != null)
			{
				sprite.Texture = EntityResource.Sprites[0];
			}
		}
	}
}
