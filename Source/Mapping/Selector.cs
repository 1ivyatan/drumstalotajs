using Godot;
using System;

namespace drumstalotajs.Mapping;

public partial class Selector : Node2D
{
	[Signal] public delegate void SelectedEventHandler(Vector2I cellPos);
	[Signal] public delegate void HoveredEventHandler(Vector2I cellPos);
	
	private Layers.GroundLayer groundLayer;
	private Sprite2D sprite;
	
	private Vector2I currentCellPos;
	
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite");
		groundLayer = GetNode<TileMapLayer>("../GroundLayer") as Layers.GroundLayer;
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouse mouseEvent)
		{
			Vector2 globalMousePos = GetGlobalMousePosition();
			Vector2 localMousePos = groundLayer.ToLocal(globalMousePos);
			Vector2I cellPos = groundLayer.LocalToMap(localMousePos);
			
			if (AllowedFilter(cellPos))
			{
				if (mouseEvent is InputEventMouseMotion mouseMotion)
				{
					if (cellPos != currentCellPos)
					{
						EmitSignal("Hovered", cellPos);
						MoveHighlighter(cellPos);
						currentCellPos = cellPos;
					}
				}
				
				if (mouseEvent is InputEventMouseButton mouseClick)
				{
					if (mouseClick.Pressed)
					{
						switch (mouseClick.ButtonIndex)
						{
							case MouseButton.Left:
								break;
						}
					}
				}
			}
			
		}
	}
	
	private bool AllowedFilter(Vector2I cellPos)
	{
		return true;
	}
	
	private void MoveHighlighter(Vector2I cellPos)
	{
		SetPosition((cellPos * groundLayer.TileSize) + new Vector2I(groundLayer.TileSize / 2, groundLayer.TileSize / 2));
	}
}
