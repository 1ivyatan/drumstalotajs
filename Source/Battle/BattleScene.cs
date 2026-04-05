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
using Drumstalotajs.Scores;

namespace Drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	public Mapping.Map Map { get; private set; }
	public StageManager StageManager { get; private set; }
	public BattleScoreManager BattleScoreManager { get; private set; }
	public Topnav Topnav { get; private set; }
	public MapMeta MapMeta { get; private set; } = null;
	
	private Modal _pauseModal;
	private Button _pause;
	private Callable _sceneChangeCall;
	private Callable _tickedCall;
	
	public override void _Ready()
	{
		var sceneManager = Nodes.GetRoot().SceneManager;
		Map = GetNode("Map") as Mapping.Map;
		BattleScoreManager = GetNode("BattleScoreManager") as BattleScoreManager;
		StageManager = GetNode("StageManager") as StageManager;
		Node overlay = GetNode("Overlay");
		Topnav = overlay.GetNode("Topnav") as Topnav;
		_pauseModal = overlay.GetNode("PauseMenu") as Modal;
		
		_sceneChangeCall = Callable.From<Managers.Scenes.SceneState>(state => {
			switch (state)
			{
				case Managers.Scenes.SceneState.RUNNING:
					_pauseModal.HideModal();
					BattleScoreManager.Resume();
					break;
				case Managers.Scenes.SceneState.PAUSED:
					_pauseModal.ShowModal();
					BattleScoreManager.Pause();
					break;
				default: break;
			}
		});
		
		_tickedCall = Callable.From<double>(remaining => {
			Topnav.Counters.TimeLeft.SetTime(remaining);
		});
		
		if (MapMeta != null)
		{
			Map.Load(MapMeta);
			BattleScoreManager.Load(Map);
			BattleScoreManager.Connect("Ticked", _tickedCall);
			sceneManager.Connect("StateChanged", _sceneChangeCall);
			BattleScoreManager.Reset();
			StageManager.DevicePlacement();
		}
	}
	
	public override void _ExitTree()
	{
		var sceneManager = Nodes.GetRoot().SceneManager;
		sceneManager.Disconnect("StateChanged", _sceneChangeCall);
		BattleScoreManager.Disconnect("Ticked", _tickedCall);
	}
	
	public void LoadLevel(LevelSetProps props)
	{
		MapMeta = props.Meta;
	}
		
	
}
