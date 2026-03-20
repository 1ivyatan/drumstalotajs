using Godot;
using System;

namespace drumstalotajs.Levels;

public partial class LevelsScene : Node2D
{
	private Mapping.Map map;
	private Managers.SceneManager sceneManager;
	private Button toStartButton;

	public override void _Ready()
	{
		sceneManager = GetNode<Node>("../") as Managers.SceneManager;
		map = GetNode<Node2D>("Map") as Mapping.Map;
		toStartButton = GetNode<Button>("UI/ToStartButton");
	//	map.Camera.FitCamera(map.GroundLayer);
		toStartButton.Pressed += () => {
			sceneManager.StartScene();
		};
	}
}
