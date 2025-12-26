using Godot;
using System;

public partial class Selector : Node2D
{
	Node2D parent;
	TileMapLayer entityLayer;
	
	Sprite2D sprite;
	
	public override void _Ready()
	{
		parent = GetParent<Node2D>();
		entityLayer = parent.GetNode<TileMapLayer>("EntityLayer");
		sprite = GetNode<Sprite2D>("Sprite");
	}
	
	void SetSelection(Vector2I cellPos)
	{
		if (sprite != null)
		{
			sprite.Visible = true;
			SetPosition((cellPos * 36) + new Vector2I(36 / 2, 36 / 2));	
		}
	}
	
	void ClearSelection()
	{
		if (sprite != null)
		{
			sprite.Visible = false;
		}
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion eventMotion)
		{
			Vector2 globalMousePos = GetGlobalMousePosition();
			Vector2 localMousePos = parent.ToLocal(globalMousePos);
			Vector2I cellPos = entityLayer.LocalToMap(localMousePos);
			
			if ((EntityType)entityLayer.GetCellAlternativeTile(cellPos) == EntityType.None)
			{
				ClearSelection();
			} else {
				SetSelection(cellPos);	
			}
		}
	}
}
