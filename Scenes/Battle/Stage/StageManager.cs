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
	
	public void SetStage(string name)
	{
		if (GetChildCount() > 0)
		{
			foreach (Node child in GetChildren())
			{
				RemoveChild(child);
			}
		}
		
		string stagePath = $"res://Scenes/Battle/Stage/Stages/{name}.tscn";
		Node stageNode = ResourceLoader.Load<PackedScene>(stagePath).Instantiate();
		AddChild(stageNode);
		
		Stage stage = stageNode as Stage;
		stage.Load(uiNode, rootNode, gridNode);
		activeStage = stage;
		
		EmitSignal(SignalName.StageChanged, name);
	}
	
	public override void _Ready()
	{
		rootNode = GetNode("../");
		gridNode = rootNode.GetNode<Node2D>("Grid");
		uiNode = rootNode.GetNode<Control>("../../");
		
		Connect("StageChanged", new Callable(uiNode, "StageChanged"));
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
