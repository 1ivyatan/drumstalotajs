using Godot;
using System;

namespace drumstalotajs;

public partial class Main : Node
{
	private Managers.SceneManager sceneManager;
	
	public override void _Ready()
	{
		sceneManager = GetNode<Node>("SceneManager") as Managers.SceneManager;
		sceneManager.StartScene();
	}
}
