using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Resources.Progress;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Managers.Scenes;
using Drumstalotajs.Managers;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	private Modal _pauseModal;
	private Button _pause;
	private Callable _sceneChangeCall;
	
	public override void _Ready()
	{
		var sceneManager = Nodes.GetRoot().SceneManager;
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
