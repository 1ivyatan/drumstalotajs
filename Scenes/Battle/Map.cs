using Godot;
using System;

public partial class Map : Node2D
{
	Node2D MapGrid;
	StageManager MapStageManager;
	Entities EntityManager;
	
	public override void _Ready()
	{
		MapStageManager = GetNode("StageManager") as StageManager;
		MapGrid = GetNode<Node2D>("Grid");
		EntityManager = MapGrid.GetNode("Entities") as Entities;
	}
	
	public void AddedDevice(Vector2I position)
	{
		EntityManager.UpdateCount(Entities.EntityType.Device);
	}
	
	public void RemovedDevice(Vector2I position)
	{
		EntityManager.UpdateCount(Entities.EntityType.Device);
	}
}
