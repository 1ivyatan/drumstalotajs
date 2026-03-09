using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Map
{
	public partial class Selector : Node2D
	{
		[Signal] public delegate void HoverChangeEventHandler(Vector2I cellPos);
		[Signal] public delegate void SelectedChangeEventHandler();
		
		public bool Disabled { get; set; }
		public SelectorFilter Filter { get; set; }
		private Vector2I CurrentCell { get; set; }
		
		private Map.Layers.GroundLayer groundLayer;
		private Map.Layers.EntityLayer entityLayer;
		
		public override void _Ready()
		{
			groundLayer = GetNode<TileMapLayer>("../GroundLayer") as Map.Layers.GroundLayer;
			entityLayer = GetNode<TileMapLayer>("../EntityLayer") as Map.Layers.EntityLayer;
			Filter = new SelectorFilter(null, Map.Layers.LayerType.ALL);
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
						if (mouseEvent is InputEventMouseMotion mouseMotion)
						{
							if (Filter.Layer == Map.Layers.LayerType.GROUND || 
							Filter.Layer == Map.Layers.LayerType.ALL)
							{
								if (CurrentCell != cellPos)
								{
									EmitSignal("HoverChange");
									CurrentCell = cellPos;
								}
							}
						}
						
						if (mouseEvent is InputEventMouseButton mouseClick)
						{
							if (mouseClick.Pressed)
							{
								if (Filter.Layer == Map.Layers.LayerType.ENTITY || 
								Filter.Layer == Map.Layers.LayerType.ALL)
								{
									EmitSignal("SelectedChange");
								}
							}
						}
						
						MoveSelector(cellPos);
						Visible = true;
					} else
					{
						Visible = false;
						return;
					}
				}
			} else
			{
				Visible = false;
			}
		}
		
		private bool FilterAllowed(Vector2I cellPos)
		{
			GD.Print(Filter.Layer);
			GD.Print(cellPos);
			
			//if (Filter.Layer == Map.Layers.LayerType.GROUND || 
			//Filter.Layer == Map.Layers.LayerType.ALL)
			//{
				if (groundLayer.GetCellAtlasCoords(cellPos) == new Vector2I(-1, -1)) return false;
			//}
			
			//if (Filter.Layer == Map.Layers.LayerType.ENTITY || 
			//Filter.Layer == Map.Layers.LayerType.ALL)
			//{
		//	}
				return true;
			//return (
			//	groundLayer.GetCellAtlasCoords(cellPos) != new Vector2I(-1, -1) &&
			//	Filter.
			//);
		}
		
		private void MoveSelector(Vector2I cellPos)
		{
			SetPosition((cellPos * groundLayer.TileSize) + new Vector2I(groundLayer.TileSize / 2, groundLayer.TileSize / 2));
		}
	}
}
