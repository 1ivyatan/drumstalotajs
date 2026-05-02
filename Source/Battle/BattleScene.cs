using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Battle.Components;
using System.Threading.Tasks;
using Drumstalotajs.Resources.Saves;

namespace Drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	[Export] public BattleTopnav BattleTopnav { get; private set; }
	[Export] public Map Map { get; private set; }
	[Export] private ScoreManager ScoreManager { get; set; }
	[Export] private PauseOverlay _pauseOverlay;
	public bool Paused { get; private set; } = false;
	
	private LevelSet _levelSet;
	private LevelProps _levelProps;
	private string _mapPath;
	
	
	[Export] private Button _fakeVictory;
	
	public override void _Ready()
	{
		
		BattleTopnav.PressedPause += () => { Pause(); };
		_pauseOverlay.PressedResume += () => { Resume(); };
		_pauseOverlay.PressedRestart += () => { Restart(); };
		_pauseOverlay.PressedExit += () => { Exit(); };
		
		_fakeVictory.Pressed += () => { RecordScore();  Exit(); };
	}
	
	private void RecordScore()
	{
		if (_levelSet != null && _levelProps != null)
		{
			var saveManager = Nodes.GetRoot().SaveManager;
			var score = new LevelScore(_levelProps);
			saveManager.AddScore(_levelSet, score);
		}
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
		_levelSet = levelSet;
		_levelProps = levelProps;
		_mapPath = levelProps.MapPath;
		Load(levelProps.MapPath);
	}
	
	public async Task Open(string mapPath)
	{
		_mapPath = mapPath;
		Load(mapPath);
	}
	
	private void Load(string mapPath)
	{
		Map.Load(mapPath);
		if (_levelProps != null)
		{
			ScoreManager.PrepareScoring(Map.CurrentLoadedMap, _levelProps);
		} else
		{
			ScoreManager.PrepareScoring(Map.CurrentLoadedMap);
		}
	}
	
	private void Restart()
	{
		if (_levelSet != null && _levelProps != null)
		{
			Nodes.GetRoot().SceneManager.Battle(_levelSet, _levelProps);
		} else if (_mapPath != null && _mapPath != "")
		{
			Nodes.GetRoot().SceneManager.Battle(_mapPath);
		}
	}
}
