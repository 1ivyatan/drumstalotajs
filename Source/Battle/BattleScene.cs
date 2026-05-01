using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Battle.Components;

namespace Drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	[Export] public BattleTopnav BattleTopnav { get; private set; }
	[Export] private PauseOverlay _pauseOverlay;
	public bool Paused { get; private set; } = false;
	
	public override void _Ready()
	{
		BattleTopnav.PressedPause += () => { Pause(); };
		
		_pauseOverlay.PressedResume += () => { Resume(); };
		_pauseOverlay.PressedRestart += () => {  };
		_pauseOverlay.PressedExit += () => {  };
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
	
	public void Open(LevelSet levelSet, LevelProps levelProps)
	{
		
	}
	
	public void Open(string mapPath)
	{
		
	}
}
