using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class Selector : Node2D
{
	public enum SelectMode
	{
		Filtered
	}
	
	[Signal]
	public delegate void ClickedOnEntityEventHandler(int entityType, Vector2I position);
	
	Node2D parent;
	TileMapLayer entityLayer;
	Sprite2D sprite;
	
	SelectMode selectMode;
	EntityType[] entityFilter;
	
	bool active;
	
	public void Enabled(bool enabled)
	{
		active = enabled;
	}
	
	public void SetSelectMode(SelectMode mode)
	{
		selectMode = mode;
	}
	
	public void SetFilter(EntityType[] whitelist)
	{
		if (whitelist == null)
		{
			entityFilter = null;
		} else
		{
			entityFilter = whitelist;
		}
	}
	
	public override void _Ready()
	{
		parent = GetParent<Node2D>();
		entityLayer = parent.GetNode<TileMapLayer>("EntityLayer");
		sprite = GetNode<Sprite2D>("Sprite");
		entityFilter = null;
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
				return;
			}
			
			switch (selectMode)
			{
				case SelectMode.Filtered:
					if (entityFilter != null && entityFilter.Length > 0 && !entityFilter.Contains(entityType))
					{
						return;
					}
					
					PositionSelector(cellPos);
					break;
				default:
					break;
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
