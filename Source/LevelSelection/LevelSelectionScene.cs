using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Editor.Components;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Cameras;
using Drumstalotajs.Utils;
using Drumstalotajs.Resources.LevelSets;

namespace Drumstalotajs.LevelSelection;

public partial class LevelSelectionScene : Node2D
{
	[Export] public Map Map { get; private set; }
	
	/* !!!!!! */
	[Export] public LevelSet _levelSet { get; private set; }
	[Export] private Button _startButton;
	
	public override void _Ready()
	{
		_startButton.Pressed += () => {Nodes.GetRoot().SceneManager.Start();}; 
		
		Map.Mode = MapMode.Lock;
		Map.Load(_levelSet.BackgroundMapPath);
		Map.Camera.Mode = CameraMode.DragOnly;
	}
	
}
