using Godot;
using System;

public partial class Main : Node
{
	SceneManager sceneManager;
	
	public override void _Ready()
	{
		sceneManager = GetNode<Node>("SceneManager") as SceneManager;
		
		sceneManager.SetScene("StartMenu", SwitchState.DESTROY);
	}
}
