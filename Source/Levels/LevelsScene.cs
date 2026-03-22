using Godot;
using System;

namespace drumstalotajs.Levels;

public partial class LevelsScene : Node2D
{
	[Export] Resources.Sets.Levels.LevelSet LevelSet;
	
	private Managers.SceneManager sceneManager;
	private LevelSelectionContainer levelSelectionContainer;
	private Mapping.Map map;
	private Button toStartButton;
	

	public override void _Ready()
	{
		sceneManager = GetNode<Node>("../") as Managers.SceneManager;
		levelSelectionContainer = GetNode<Control>("UI/LevelSelectionContainer") as LevelSelectionContainer;
		map = GetNode<Node2D>("Map") as Mapping.Map;
		toStartButton = GetNode<Button>("UI/ToStartButton");
		map.Mode = Mapping.Map.MapMode.LOCK;
		map.LoadMap(LevelSet.BackgroundMap);
		map.Camera.Calibrate(map.GroundLayer);
		map.Camera.FitCamera(map.GroundLayer);
		levelSelectionContainer.LoadLevelSelection(LevelSet);
		toStartButton.Pressed += () => {
			sceneManager.StartScene();
		};
		
	}
}
