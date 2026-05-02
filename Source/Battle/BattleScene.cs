using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Battle.Components;
using System.Threading.Tasks;
using Drumstalotajs.Resources.Saves;
using Drumstalotajs.Battle.Stages;

namespace Drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	[Export] public BattleTopnav BattleTopnav { get; private set; }
	[Export] public Map Map { get; private set; }
	[Export] private ScoreManager ScoreManager { get; set; }
	[Export] public StageManager StageManager { get; private set; }

	[Export] private PauseOverlay _pauseOverlay;
	public bool Paused { get; private set; } = false;
	private string _mapPath;
	
	[Export] private Button _fakeVictory;
	
	public override void _Ready()
	{
		
		BattleTopnav.PressedPause += () => { Pause(); };
		_pauseOverlay.PressedResume += () => { Resume(); };
		_pauseOverlay.PressedRestart += () => { Restart(); };
		_pauseOverlay.PressedExit += () => { Exit(); };
		
		_fakeVictory.Pressed += () => { ScoreManager.RecordScore();  Exit(); };
	}
	
	private void Exit()
	{
		Nodes.GetRoot().SceneManager.LevelSelection();
	}
	
	private void Pause()
	{
		Nodes.GetRoot().SceneManager.PauseScene();
		Paused = true;
		_pauseOverlay.Visible = true;
	}
	
	private void Resume()
	{
		_pauseOverlay.Visible = false;
		Nodes.GetRoot().SceneManager.ResumeScene();
		Paused = false;
	}
	
	public async Task Open(LevelSet levelSet, LevelProps levelProps)
	{
		_mapPath = levelProps.MapPath;
		
		Map.Load(_mapPath);
		
		if (levelProps != null)
		{
			ScoreManager.PrepareScoring(Map.CurrentLoadedMap, levelSet, levelProps);
		} else
		{
			ScoreManager.PrepareScoring(Map.CurrentLoadedMap);
		}
	}
	
	public async Task Open(string mapPath)
	{
		_mapPath = mapPath;
		Map.Load(_mapPath);
		ScoreManager.PrepareScoring(Map.CurrentLoadedMap);
	}
	
	private void Restart()
	{
		if (ScoreManager.LevelSet != null && ScoreManager.LevelProps != null)
		{
			Nodes.GetRoot().SceneManager.Battle(ScoreManager.LevelSet, ScoreManager.LevelProps);
		} else if (_mapPath != null && _mapPath != "")
		{
			Nodes.GetRoot().SceneManager.Battle(_mapPath);
		}
	}
}
