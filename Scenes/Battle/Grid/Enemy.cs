using Godot;
using System;

public partial class Enemy : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventButton)
		{
			if (eventButton.Pressed)
			{
				GD.Print("clicked on");
			}
		}
	}
}
