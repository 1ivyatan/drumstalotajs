using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Battle.Components;
using System.Threading.Tasks;

namespace Drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	[Export] public BattleTopnav BattleTopnav { get; private set; }
	[Export] private PauseOverlay _pauseOverlay;
	public bool Paused { get; private set; } = false;
	
	private LevelSet _levelSet;
	private LevelProps _levelProps;
	private string _mapPath;
	
	public override void _Ready()
	{
		BattleTopnav.PressedPause += () => { Pause(); };
		_pauseOverlay.PressedResume += () => { Resume(); };
		_pauseOverlay.PressedRestart += () => { Restart(); };
		_pauseOverlay.PressedExit += () => { Exit(); };
		Load();
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
		Load();
	}
	
	public async Task Open(string mapPath)
	{
		_mapPath = mapPath;
		Load();
	}
	
	private void Load()
	{
		
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
