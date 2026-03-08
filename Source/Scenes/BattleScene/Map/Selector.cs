using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Map
{
	public partial class Selector : Node2D
	{
		public bool Disabled { get; set; }
		private Map.Layers.GroundLayer groundLayer;
		
		public override void _Ready()
		{
			groundLayer = GetNode<TileMapLayer>("../GroundLayer") as Map.Layers.GroundLayer;
		}
		
		public override void _UnhandledInput(InputEvent @event)
		{
			if (!Disabled)
			{
				if (@event is InputEventMouse mouseEvent)
				{
					Vector2 globalMousePos = GetGlobalMousePosition();
					Vector2 localMousePos = groundLayer.ToLocal(globalMousePos);
					Vector2I cellPos = groundLayer.LocalToMap(localMousePos);
					MoveSelector(cellPos);
				}
				
				Visible = true;
			} else
			{
				Visible = false;
			}
		}
		
		private void MoveSelector(Vector2I cellPos)
		{
			SetPosition((cellPos * groundLayer.TileSize) + new Vector2I(groundLayer.TileSize / 2, groundLayer.TileSize / 2));
		}
	}
}
