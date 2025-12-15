using Godot;
using System;
using System.Collections;

public partial class Main : Node
{
	Node currentScene;
	Control globalHud;
	Fade fade;
	
	public override void _Ready()
	{
		currentScene = GetNode<Node>("Scene");
		globalHud = GetNode<Control>("GlobalHud");
		fade = globalHud.GetNode<Control>("Fade") as Fade;
		
		SetScene("res://Scenes/Battle.tscn", SwitchState.DESTROY, 0.5f);
	}
	
	
	public void SetScene(string ScenePath, SwitchState mode, float delay)
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
		
		fade.FadeOut(1.0f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
