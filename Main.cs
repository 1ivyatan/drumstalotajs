using Godot;
using System;

public partial class Main : Node
{
	SceneManager sceneManager;
	
	public override void _Ready()
	{
		sceneManager = GetNode<Node>("SceneManager") as SceneManager;
		sceneManager.SetScene("StartMenu", SwitchState.DESTROY);
		
		Node startMenuChild = sceneManager.GetCurrentScene().GetChild(0);
		startMenuChild.Connect("StartMenuStart", new Callable(this, nameof(LevelSelect)));
		startMenuChild.Connect("StartMenuExit", new Callable(this, nameof(Exit)));
	}
	
	void LevelSelect() {
		sceneManager.SetScene("LevelSelect", SwitchState.DESTROY);
	}
	
	void Exit() {
		GetTree().Quit();
	}
}
