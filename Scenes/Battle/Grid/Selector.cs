using Godot;
using System;

public partial class Selector : Node2D
{
	Node2D parent;
	TileMapLayer entityTile;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parent = GetParent<Node2D>();
		entityTile = parent.GetNode<TileMapLayer>("EntityLayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion eventMotion)
		{
			Vector2 globalMousePos = GetGlobalMousePosition();
			Vector2 localMousePos = parent.ToLocal(globalMousePos);
			Vector2I cellPos = entityTile.LocalToMap(localMousePos);
			
			SetPosition(localMousePos);
			
			GD.Print(localMousePos);
		}
	}
}
