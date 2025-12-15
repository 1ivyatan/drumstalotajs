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
		
		SetScene("res://Scenes/Battle.tscn");
	}
	
	public void SetScene(string ScenePath)
	{
		Node newScene = ResourceLoader.Load<PackedScene>(ScenePath).Instantiate();
		
		currentScene.AddChild(newScene);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
