using Godot;
using System;

public partial class Map : Node2D
{
	StageManager stageManager;
	
	public override void _Ready()
	{
		stageManager = GetNode("StageManager") as StageManager;
	}
	
	public override void _Input(InputEvent @event)
	{
		stageManager.DelegateInput(@event);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
