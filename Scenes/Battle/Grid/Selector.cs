using Godot;
using System;
using System.Collections.Generic;

public partial class Selector : Node2D
{
	[Signal]
	public delegate void ClickedOnEntityEventHandler(int entityType, Vector2I position);
	
	Node2D parent;
	TileMapLayer entityLayer;
	Sprite2D sprite;
	
	List<EntityType> entityFilter;
	
	bool active;
	
	public void Enabled(bool enabled)
	{
		active = enabled;
	}
	
	public override void _Ready()
	{
		parent = GetParent<Node2D>();
		entityLayer = parent.GetNode<TileMapLayer>("EntityLayer");
		sprite = GetNode<Sprite2D>("Sprite");
		entityFilter = new List<EntityType>();
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
		if (!active)
		{
			return;
		}
		
		if (@event is InputEventMouse mouseEvent)
		{
			Vector2 globalMousePos = GetGlobalMousePosition();
			Vector2 localMousePos = parent.ToLocal(globalMousePos);
			Vector2I cellPos = entityLayer.LocalToMap(localMousePos);
			EntityType entityType = (EntityType)entityLayer.GetCellAlternativeTile(cellPos);
			
			if (entityType == EntityType.None)
			{
				HideSelector();
			} else {
				PositionSelector(cellPos);	
			}
			
			if (mouseEvent is InputEventMouseButton mouseClick)
			{
				if (mouseClick.Pressed)
				{
					EmitSignal(SignalName.ClickedOnEntity, (int)entityType, cellPos);
				}
			}
		}
	}
}
