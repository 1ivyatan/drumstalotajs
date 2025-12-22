using Godot;
using System;

public partial class Map : Node2D
{
	StageManager stageManager;
	
	public override void _Ready()
	{
		stageManager = GetNode("StageManager") as StageManager;
	}
	
	public void AddedDevice(Vector2I position)
	{
		GD.Print("Added at " + position);
	}
	
	public void RemovedDevice(Vector2I position)
	{
		GD.Print("Removed at " + position);
	}
}
