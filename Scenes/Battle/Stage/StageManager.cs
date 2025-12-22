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
	
	public override void _Ready()
	{
		stageNodes = new List<Stage>();
		rootNode = GetNode("../");
		gridNode = rootNode.GetNode<Node2D>("Grid");
			
		foreach (Node child in GetChildren()) 
		{
			if (child is Stage)
			{
				stageNodes.Add(child as Stage);
				stageNodes.Last().SetMapRootNode(rootNode);
				stageNodes.Last().SetMapGridNode(gridNode);
			}
		}
		
		activeStage = stageNodes[0];
		activeStage.Load();
	}
	
	public override void _Input(InputEvent @event)
	{
		activeStage.Input(@event);
	}
}
