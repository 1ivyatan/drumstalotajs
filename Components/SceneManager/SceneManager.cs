using Godot;
using System;
using System.Collections;

public partial class SceneManager : Node
{
	Node currentScene;
	Control globalHud;
	Fade fade;
	
	public Node GetCurrentScene() {
		return currentScene;
	}
	
	public override void _Ready()
	{
		currentScene = GetNode<Node>("Scene");
		globalHud = GetNode<Control>("GlobalHud");
		fade = globalHud.GetNode<Control>("Fade") as Fade;
	}
	
	void LoadScene(string sceneName, SwitchState mode) {
		switch (mode) {
			case SwitchState.DESTROY:
				foreach (Node child in currentScene.GetChildren()) {
					child.QueueFree();
  					currentScene.RemoveChild(child);
				}
				break;
			default:
				break;	
		}
		
		string scenePath = $"res://Scenes/{sceneName}/{sceneName}.tscn";
		Node newScene = ResourceLoader.Load<PackedScene>(scenePath).Instantiate();
		currentScene.AddChild(newScene);
	}
	
	public void SetScene(string sceneName, SwitchState mode)
	{
	//	if (fadeOut) {
	//		fade.FadeOut(5.0f);
	//	}
		
		LoadScene(sceneName, mode);
	//	fade.FadeIn(5.0f);
	}
}
