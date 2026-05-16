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
using Drumstalotajs.Mapping.Entities;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	[Export] public BattleTopnav BattleTopnav { get; private set; }
	[Export] public Map Map { get; private set; }
	[Export] public ScoreManager ScoreManager { get; private set; }
	[Export] public StageManager StageManager { get; private set; }
	[Export] public Counters Counters { get; private set; }

	[Export] private PauseOverlay _pauseOverlay;
	[Export] private Label _measureLabel;
	[Export] private Label _positionLabel;
	[Export] private Label _altitiudeLabel;

	public bool Paused { get; private set; } = false;
	private string _mapPath;
	
	public override void _Ready()
	{
		BattleTopnav.PressedPause += () => { Pause(); };
		_pauseOverlay.PressedResume += () => { Resume(); };
		_pauseOverlay.PressedRestart += () => { Restart(); };
		_pauseOverlay.PressedExit += () => { Exit(); };
		Map.Camera.ShiftTop((int)BattleTopnav.Size.Y);
		StageManager.DevicePlacement();
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		var pos = Map.ViewportMouseToMap();
		var newpos = new Vector2(pos.X, Math.Abs(pos.Y));
		_positionLabel.Text = $"{newpos}";
	}
	
	public void Exit()
	{
		/* hack */
		Map.ProcessMode = ProcessModeEnum.Disabled;
		ScoreManager.ProcessMode = ProcessModeEnum.Disabled;
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
		
		await Map.Load(_mapPath);
		
		if (levelProps != null)
		{
			ScoreManager.PrepareScoring(Map.CurrentLoadedMap, levelSet, levelProps);
		} else
		{
			ScoreManager.PrepareScoring(Map.CurrentLoadedMap);
		}
		
		_measureLabel.Text = $"{Map.CurrentLoadedMap.MetersPerCell.X}m";
	}
	
	public async Task Open(string mapPath)
	{
		_mapPath = mapPath;
		await Map.Load(_mapPath);
		_measureLabel.Text = $"{Map.CurrentLoadedMap.MetersPerCell.X}m";
		_altitiudeLabel.Text = $"{Map.CurrentLoadedMap.GroundLayer.BaseHeight}m";
		ScoreManager.PrepareScoring(Map.CurrentLoadedMap);
	}
	
	public void Restart()
	{
		if (ScoreManager.IsInLevel())
		{
			Nodes.GetRoot().SceneManager.Battle(ScoreManager.LevelSet, ScoreManager.LevelProps);
		} else if (_mapPath != null && _mapPath != "")
		{
			Nodes.GetRoot().SceneManager.Battle(_mapPath);
		}
	}
}
