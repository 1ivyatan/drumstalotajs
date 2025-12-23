using Godot;
using System;

public partial class Map : Node2D
{
	Node2D MapGrid;
	StageManager MapStageManager;
	EntityLayer EntityManager;
	
	public override void _Ready()
	{
		MapStageManager = GetNode("StageManager") as StageManager;
		MapGrid = GetNode<Node2D>("Grid");
		EntityManager = MapGrid.GetNode("EntityLayer") as EntityLayer;
	}
	
	public void AddedDevice(Vector2I position)
	{
		EntityManager.UpdateCount(EntityType.Device);
	}
	
	public void RemovedDevice(Vector2I position)
	{
		EntityManager.UpdateCount(EntityType.Device);
	}
}
