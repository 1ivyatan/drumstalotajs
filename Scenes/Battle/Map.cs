using Godot;
using System;

public partial class Map : Node2D
{
	int deviceMaxCount = 2;
	
	StageManager stageManager;
	
	public override void _Ready()
	{
		stageManager = GetNode("StageManager") as StageManager;
	}
	
	public void AddDevice(Vector2 position)
	{
		GD.Print("added device");
		GD.Print(position);
	}
}
