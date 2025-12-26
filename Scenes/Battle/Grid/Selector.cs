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
	
	void SetSelectedEntity()
	{
		
	}
	
	void PositionSelector(Vector2I cellPos)
	{
		if (sprite != null)
		{
			sprite.Visible = true;
			SetPosition((cellPos * 36) + new Vector2I(36 / 2, 36 / 2));	
		}
	}
	
	void HideSelector()
	{
		if (sprite != null)
		{
			sprite.Visible = false;
		}
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouse mouseEvent)
		{
			Vector2 globalMousePos = GetGlobalMousePosition();
			Vector2 localMousePos = parent.ToLocal(globalMousePos);
			Vector2I cellPos = entityLayer.LocalToMap(localMousePos);
			EntityType entity = (EntityType)entityLayer.GetCellAlternativeTile(cellPos);
			
			if (entity == EntityType.None)
			{
				HideSelector();
			} else {
				PositionSelector(cellPos);	
			}
			
			if (mouseEvent is InputEventMouseButton mouseClick)
			{
				if (mouseClick.Pressed)
				{
				//	(entityLayer as EntityLayer)
				//	GD.Print(cellPos);
				}
			}
		}
	}
}
