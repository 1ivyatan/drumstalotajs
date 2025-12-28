using Godot;
using System;

public partial class Selector : Node2D
{
	public enum SelectorFilterMode
	{
		None,
		Fitlered,
		All
	}
	
	public SelectorFilterMode SelectorMode
	{
		get;
		set;
	}
	
	private Node2D grid;
	private TileMapLayer entityLayer;
	
	public override void _Ready()
	{
		this.grid = GetNode<Node2D>("../Grid");
		this.entityLayer = this.grid.GetNode<TileMapLayer>("EntityLayer");
	}
	
	public override void _Input(InputEvent @event)
	{
		if (this.SelectorMode == SelectorFilterMode.None)
		{
			return;
		}
		
		if (@event is InputEventMouse mouseEvent)
		{
			Vector2 globalMousePos = GetGlobalMousePosition();
			Vector2 localMousePos = this.grid.ToLocal(globalMousePos);
			Vector2I cellPos = this.entityLayer.LocalToMap(localMousePos);
			
			SetPosition((cellPos * 80) + new Vector2I(80 / 2, 80 / 2));	
		}
	}
}
