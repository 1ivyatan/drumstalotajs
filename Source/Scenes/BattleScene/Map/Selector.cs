using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Map
{
	public partial class Selector : Node2D
	{
		[Signal] public delegate void HoverChangeEventHandler(Vector2I cellPos);
		
		public bool Disabled { get; set; }
		private Map.Layers.GroundLayer groundLayer;
		private Vector2I CurrentCell { get; set; }
		
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
					
					if (FilterAllowed(cellPos))
					{
						MoveSelector(cellPos);
						Visible = true;
					} else
					{
						Visible = false;
						return;
					}
					
					if (mouseEvent is InputEventMouseMotion mouseMotion)
					{
						if (CurrentCell != cellPos)
						{
							EmitSignal("HoverChange");
							CurrentCell = cellPos;
						}
					}
				}
			} else
			{
				Visible = false;
			}
		}
		
		private bool FilterAllowed(Vector2I cellPos)
		{
			return groundLayer.GetCellAtlasCoords(cellPos) != new Vector2I(-1, -1);
		}
		
		private void MoveSelector(Vector2I cellPos)
		{
			SetPosition((cellPos * groundLayer.TileSize) + new Vector2I(groundLayer.TileSize / 2, groundLayer.TileSize / 2));
		}
	}
}
