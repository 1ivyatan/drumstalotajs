using Godot;
using System;

namespace drumstalotajs.Mapping;

public partial class Selector : Node2D
{
	[Signal] public delegate void SelectedEventHandler(Vector2I cellPos);
	[Signal] public delegate void HoveredEventHandler(Vector2I cellPos);
	
	public bool Locked { get; set; } = false;
	public bool Readonly { get; set; } = true;
	
	private Layers.GroundLayer groundLayer;
	private Sprite2D sprite;
	
	private Vector2I currentCellPos;
	
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite");
		groundLayer = GetNode<TileMapLayer>("../GroundLayer") as Layers.GroundLayer;
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (!Locked)
		{
			if (@event is InputEventMouse mouseEvent)
			{
				Vector2 localPos = GetLocalPos();
				Vector2I cellPos = GetCellPos(localPos);
				if (AllowedFilter(cellPos))
				{
					if (mouseEvent is InputEventMouseMotion mouseMotion)
					{
						if (cellPos != currentCellPos)
						{
							MoveHighlighter(cellPos);
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
				} else
				{
					HideHighlighter();
				}
			}
		} else
		{
			HideHighlighter();
		}
	}
	
	public void Reposition()
	{
		if (!Locked)
		{
			Vector2 localPos = GetLocalPos();
			Vector2I cellPos = GetCellPos(localPos);
			MoveHighlighter(cellPos);
		}
	}
	
	private Vector2 GetLocalPos()
	{
		Vector2 globalMousePos = GetGlobalMousePosition();
		return groundLayer.ToLocal(globalMousePos);
	}
	
	private Vector2I GetCellPos(Vector2 localMousePos)
	{
		return groundLayer.LocalToMap(localMousePos);
	}
	
	private bool AllowedFilter(Vector2I cellPos)
	{
		if (Readonly)
		{
			return true;
		} else
		{
			return true;
		}
	}
	
	private void HideHighlighter()
	{
		Visible = false;
	}
	
	private void MoveHighlighter(Vector2I cellPos)
	{
		SetPosition((cellPos * groundLayer.TileSize) + new Vector2I(groundLayer.TileSize / 2, groundLayer.TileSize / 2));
		currentCellPos = cellPos;
		Visible = true;
		EmitSignal("Hovered", cellPos);
	}
}
