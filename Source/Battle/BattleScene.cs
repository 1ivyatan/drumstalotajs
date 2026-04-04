using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Resources.Progress;
using Drumstalotajs.Resources.Mapping;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Managers.Scenes;
using Drumstalotajs.Managers;
using Drumstalotajs.Utils;
using Drumstalotajs.Battle.Stages;

namespace Drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	public Mapping.Map Map { get; private set; }
	public StageManager StageManager { get; private set; }
	public Topnav Topnav { get; private set; }
	public MapMeta MapMeta { get; private set; } = null;
	
	private Modal _pauseModal;
	private Button _pause;
	private Callable _sceneChangeCall;
	
	public override void _Ready()
	{
		var sceneManager = Nodes.GetRoot().SceneManager;
		Map = GetNode("Map") as Mapping.Map;
		StageManager = GetNode("StageManager") as StageManager;
		Node overlay = GetNode("Overlay");
		Topnav = overlay.GetNode("Topnav") as Topnav;
		_pauseModal = overlay.GetNode("PauseMenu") as Modal;
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
		
		if (MapMeta != null)
		{
			Map.Load(MapMeta);
		}
		
		//_mapResource = 
		sceneManager.Connect("StateChanged", _sceneChangeCall);
		StageManager.DevicePlacement();
	}
	
	public override void _ExitTree()
	{
		var sceneManager = Nodes.GetRoot().SceneManager;
		sceneManager.Disconnect("StateChanged", _sceneChangeCall);
	}
	
	public void LoadLevel(LevelSetProps props)
	{
		MapMeta = props.Meta;
	}
		
	
}
