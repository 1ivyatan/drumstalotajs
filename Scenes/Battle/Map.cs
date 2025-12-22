using Godot;
using System;

public partial class Map : Node2D
{
	StageManager stageManager;
	
	public override void _Ready()
	{
		stageManager = GetNode("StageManager") as StageManager;
	}
	
	public void ToggledDeviceInGrid(Vector2I position)
	{
		GD.Print("Toggled at " + position);
	}
}
