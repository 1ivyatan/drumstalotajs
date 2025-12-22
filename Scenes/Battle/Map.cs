using Godot;
using System;

public partial class Map : Node2D
{
	StageManager stageManager;
	
	public override void _Ready()
	{
		stageManager = GetNode("StageManager") as StageManager;
	}
	
	public void ToggleDevice(Vector2I position)
	{
		Entities entities = GetNode("Grid/Entities") as Entities;
		entities.ToggleDevice(position);
	}
}
