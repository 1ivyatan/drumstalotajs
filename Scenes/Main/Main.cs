using Godot;
using System;

public partial class Main : Node
{
	Node rootNode;
	Node currentScene;
	Control globalHud;
	
	public override void _Ready()
	{
		currentScene = GetNode<Node>("Scene");
		globalHud = GetNode<Control>("GlobalHud");
		
		SetScene("res://Scenes/Battle.tscn", SwitchState.DESTROY);
	}
	
	public void SetScene(string ScenePath, SwitchState mode)
	{
		switch (mode) {
			case SwitchState.DESTROY:
				foreach (Node child in currentScene.GetChildren()) {
					child.QueueFree();
				}
				break;
			default:
				break;	
		}
		
		Node newScene = ResourceLoader.Load<PackedScene>(ScenePath).Instantiate();
		
		currentScene.AddChild(newScene);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
