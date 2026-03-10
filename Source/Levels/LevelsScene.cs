using Godot;
using System;

namespace drumstalotajs.Levels;

public partial class LevelsScene : Node2D
{
	private Managers.SceneManager sceneManager;

	public override void _Ready()
	{
		sceneManager = GetNode<Node>("../") as Managers.SceneManager;
	}
}
