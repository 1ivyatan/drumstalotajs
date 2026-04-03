using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Resources.Progress;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Managers.Scenes;
using Drumstalotajs.Managers;
using Drumstalotajs.Utils;
using Drumstalotajs.Battle.Stages;

namespace Drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	private Map Map { get; set; }
	private StageManager StageManager { get; set; }
	private Modal _pauseModal;
	private Button _pause;
	private Callable _sceneChangeCall;
	
	public override void _Ready()
	{
		var sceneManager = Nodes.GetRoot().SceneManager;
		Map = GetNode("Map") as Map;
		StageManager = GetNode("StageManager") as StageManager;
		Node overlay = GetNode("Overlay");
		_pauseModal = overlay.GetNode("PauseMenu") as Modal;
		_pause = overlay.GetNode<Button>("Topnav/HBoxContainer/Pause");
		_sceneChangeCall = Callable.From<Managers.Scenes.SceneState>(state => {
			switch (state)
			{
				case Managers.Scenes.SceneState.RUNNING:
					_pauseModal.HideModal();
					break;
				case Managers.Scenes.SceneState.PAUSED:
					_pauseModal.ShowModal();
					break;
				default: break;
			}
		});
		sceneManager.Connect("StateChanged", _sceneChangeCall);
		_pause.Pressed += () =>
		{
			sceneManager.PauseScene();
		};
		StageManager.DevicePlacement();
	}
	
	public override void _ExitTree()
	{
		var sceneManager = Nodes.GetRoot().SceneManager;
		sceneManager.Disconnect("StateChanged", _sceneChangeCall);
	}
	
	public void LoadLevel(LevelSetProps props)
	{
		
	}
		
	
}
