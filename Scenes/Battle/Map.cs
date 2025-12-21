using Godot;
using System;

public partial class Map : Node2D
{
	Stage stage;
	
	public override void _Ready()
	{
		stage = GetNode("Stage") as Stage;
	}
	
	public override void _Input(InputEvent @event)
	{
		stage.DelegateInput(@event);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
