using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class StageManager : Node2D
{
	[Signal]
	public delegate void StageChangedEventHandler(string name);
	
	Control uiNode;
	Node rootNode;
	Node2D gridNode;
	
	Stage activeStage;
	string activeStageName;
	
	public string ActiveStageName
	{
		get { return activeStageName; }
	}
	
	public void SetStage(string name)
	{
		if (activeStage != null)
		{
			activeStage.CloseStage();
		}
		
		if (GetChildCount() > 0)
		{
			foreach (Node child in GetChildren())
			{
				child.QueueFree();
				RemoveChild(child);
			}
		}
		
		string stagePath = $"res://Scenes/Battle/Stage/Stages/{name}.tscn";
		Node stageNode = ResourceLoader.Load<PackedScene>(stagePath).Instantiate();
		AddChild(stageNode);
		
		Stage stage = stageNode as Stage;
		stage.Load(uiNode, rootNode, gridNode);
		activeStage = stage;
		activeStageName = name;
		
		EmitSignal(SignalName.StageChanged, name);
	}
	
	public override void _Ready()
	{
		rootNode = GetNode("../");
		gridNode = rootNode.GetNode<Node2D>("Grid");
		uiNode = rootNode.GetNode<Control>("../../");
		activeStageName = null;
		
		Connect("StageChanged", new Callable(uiNode, "StageChanged"));
	}
	
	public override void _Input(InputEvent @event)
	{
		if (activeStage != null)
		{
			activeStage.Input(@event);	
		}
	}
}
