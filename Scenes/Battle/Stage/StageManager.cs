using Godot;
using System;
using System.Collections.Generic;

public partial class StageManager : Node2D
{
	List<Stage> stageNodes;
	Stage activeStage;
	
	public override void _Ready()
	{
		stageNodes = new List<Stage>();
			
		foreach (Node child in GetChildren()) 
		{
			if (child is Stage)
			{
				stageNodes.Add(child as Stage);
			}
		}
		
		activeStage = stageNodes[0];
	}
	
	public override void _Input(InputEvent @event)
	{
		activeStage.Input(@event);
	}
}
