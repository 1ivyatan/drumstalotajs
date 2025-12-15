using Godot;
using System;
using System.Collections;

public partial class SceneManager : Node
{
	Node currentScene;
	Control globalHud;
	Fade fade;
	
	public override void _Ready()
	{
		currentScene = GetNode<Node>("Scene");
		globalHud = GetNode<Control>("GlobalHud");
		fade = globalHud.GetNode<Control>("Fade") as Fade;
		
		SetScene("res://Scenes/Battle.tscn", SwitchState.DESTROY);
	}
	
	void LoadScene(string scenePath, SwitchState mode) {
		switch (mode) {
			case SwitchState.DESTROY:
				foreach (Node child in currentScene.GetChildren()) {
					child.QueueFree();
				}
				break;
			default:
				break;	
		}
		
		Node newScene = ResourceLoader.Load<PackedScene>(scenePath).Instantiate();
		currentScene.AddChild(newScene);
	}
	
	public async void SetScene(string scenePath, SwitchState mode)
	{
	//	if (fadeOut) {
	//		fade.FadeOut(5.0f);
	//	}
		
		LoadScene(scenePath, mode);
	//	fade.FadeIn(5.0f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
