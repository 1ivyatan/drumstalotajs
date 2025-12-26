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
		
		LoadStage("DevicePlacing");
	}
	
	public void LoadStage(string name)
	{
		string oldStageName = MapStageManager.ActiveStageName;
		
		GD.Print(oldStageName);
		
		switch (oldStageName)
		{
			case "DevicePlacing":
				/* cleanups, checks */
				break;	
			default:
				break;
		}
		
		MapStageManager.SetStage(name);
	}
	
	public void LoadLevel(string name)
	{
		string path = $"res://Assets/Levels/{name}.json";
		
		using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
		string content = file.GetAsText();
		
		GD.Print(content);
		
		Level level = new Level();
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
