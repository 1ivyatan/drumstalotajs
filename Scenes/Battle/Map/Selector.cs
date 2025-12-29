using Godot;
using System;
using System.Linq;

public partial class Selector : Node2D
{
	[Signal]
	public delegate void EntitySelectedEventHandler(int entityType, Vector2I position);
	
	public enum SelectorLayer
	{
		All,
		Ground,
		Entity
	}
	
	public enum SelectorFilterMode
	{
		None,
		Fitlered,
		All
	}
	
	public SelectorLayer Layer
	{
		get;
		set;
	}
	
	public SelectorFilterMode SelectorMode
	{
		get;
		set;
	}
	
	public Entity.EntityType[] EntityTypeFilter
	{
		get;
		set;
	}
	
	private Node2D grid;
	private TileMapLayer entityLayer;
	private Sprite2D sprite;
	
	public override void _Ready()
	{
		this.grid = GetNode<Node2D>("../Grid");
		this.entityLayer = this.grid.GetNode<TileMapLayer>("EntityLayer");
		this.sprite = this.GetNode<Sprite2D>("Sprite");
	}
	
	public override void _Input(InputEvent @event)
	{	
		if (@event is InputEventMouse mouseEvent)
		{
			if (this.SelectorMode == SelectorFilterMode.None)
			{
				this.sprite.Visible = false;
				return;
			}
			
			Vector2 globalMousePos = GetGlobalMousePosition();
			Vector2 localMousePos = this.grid.ToLocal(globalMousePos);
			Vector2I cellPos = this.entityLayer.LocalToMap(localMousePos);
			
			switch(this.Layer)
			{
				case SelectorLayer.Entity:
					Entity.EntityType selectedEntityType = (Entity.EntityType)this.entityLayer.GetCellAlternativeTile(cellPos);
					
					if (
						selectedEntityType == Entity.EntityType.None ||
						this.SelectorMode == SelectorFilterMode.Fitlered &&
						(
							this.EntityTypeFilter != null &&
							this.EntityTypeFilter.Length > 0 &&
							!this.EntityTypeFilter.Contains(selectedEntityType)
						)
					)
					{
						this.sprite.Visible = false;
						return;
					}
					
					break;
				default:
					break;
			}
			
			SetPosition((cellPos * 80) + new Vector2I(80 / 2, 80 / 2));	
			this.sprite.Visible = true;
			
			if (mouseEvent is InputEventMouseButton mouseClick)
			{
				if (mouseClick.Pressed)
				{
					switch(this.Layer)
					{
						case SelectorLayer.Entity:
							
							break;
						default:
							break;
					}
					//EmitSignal(SignalName.Selected, (int)entityType, cellPos);
				}
			}
		}
	}
}
