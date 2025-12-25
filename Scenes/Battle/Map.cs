using Godot;
using System;

public partial class Map : Node2D
{
	Node2D MapGrid;
	StageManager MapStageManager;
	EntityLayer entityManager;
	
	public override void _Ready()
	{
		MapStageManager = GetNode("StageManager") as StageManager;
		MapGrid = GetNode<Node2D>("Grid");
		entityManager = MapGrid.GetNode("EntityLayer") as EntityLayer;
	}
	
	public void AddedDevice(Vector2I position)
	{
		
	//	GD.Print(EntityManager.GetEntitiesOfType(EntityType.Device).Count + " / " + 
	//			 EntityManager.GetEntitiesOfType(EntityType.DevicePlaceholder).Count);
	}
	
	public void RemovedDevice(Vector2I position)
	{
	//	GD.Print(EntityManager.GetEntitiesOfType(EntityType.Device).Count + " / " + 
	//			 EntityManager.GetEntitiesOfType(EntityType.DevicePlaceholder).Count);
	}
}
