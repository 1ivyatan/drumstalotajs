using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class StageManager : Node2D
{
	Node rootNode;
	Node2D gridNode;
	List<Stage> stageNodes;
	Stage activeStage;
	
	public void SetStage(string name)
	{
		string stagePath = $"res://Scenes/Battle/Stage/Stages/{name}.tscn";
		Node stageNode = ResourceLoader.Load<PackedScene>(stagePath).Instantiate();
		AddChild(stageNode);
		
		Stage stage = stageNode as Stage;
		stage.MapRootNode = rootNode;
		stage.MapGridNode = gridNode;
		
		stage.Load();
		activeStage = stage;
	}
	
	public override void _Ready()
	{
		rootNode = GetNode("../");
		gridNode = rootNode.GetNode<Node2D>("Grid");
		SetStage("DevicePlacing");
	}
	
	public override void _Input(InputEvent @event)
	{
		if (activeStage != null)
		{
			activeStage.Input(@event);	
		}
		
	}
}
