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
using Drumstalotajs.Components;

namespace Drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	[Export] public BattleTopnav BattleTopnav { get; private set; }
	[Export] public Map Map { get; private set; }
	[Export] public ScoreManager ScoreManager { get; private set; }
	[Export] public StageManager StageManager { get; private set; }
	[Export] public Counters Counters { get; private set; }
	[Export] public RulerOverlay RulerOverlay { get; private set; }
	[Export] public DebugOverlay DebugOverlay { get; private set; }

	[Export] private PauseOverlay _pauseOverlay;
	[Export] private MilPositionContainer _milPositionContainer;
	[Export] private Label _measureLabel;
	[Export] private Label _altitiudeLabel;
	[Export] private Control _switchContainer;

	public bool Paused { get; private set; } = false;

	private string _mapPath;
	private Vector2I _mapGrid = Vector2I.Zero;
	
	public override void _Ready()
	{
		GetWindow().SizeChanged += AdjustTitlebar;
		BattleTopnav.PressedPause += () => { Pause(); };
		BattleTopnav.RulerToggled += (bool toggle) => {
			if (toggle)
			{
				Nodes.GetRoot().ToastManager.SpawnOne("Right click two positions to draw a ruler");
				Map.MarkerLayer.ActivateBrush();
			} else
			{
				Map.MarkerLayer.HideBrush();
				Nodes.GetRoot().ToastManager.Clear();
			}
			Map.MarkerLayer.Visible = toggle;
			RulerOverlay.Visible = toggle;
		};
		
		BattleTopnav.DebugToggled += (bool toggle) => {
			if ( Utilities.Debug.IsDebug() )
			{
				DebugOverlay.Visible = toggle;
			} else DebugOverlay.Visible = false;
		};
		
		DebugOverlay.AppliedDebugCheat += () => {
			DebugOverlay.Visible = false;
			BattleTopnav.ToggleDebugButton(false);
		};
		
		_pauseOverlay.PressedResume += () => { Resume(); };
		_pauseOverlay.PressedRestart += () => { Restart(); };
		_pauseOverlay.PressedExit += () => { Exit(); };
		AdjustTitlebar();
		Map.Camera.ShiftTop((int)BattleTopnav.Size.Y);
		StageManager.DevicePlacement();
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseMotion)
		{
			_milPositionContainer.UpdateCoords();
		}
	}
	
	private void AdjustTitlebar()
	{
		var size = GetWindow().Size;
		if (size.X < 620)
		{
			BattleTopnav.ToggleTitle(false);
		} else if (size.X < 850)
		{
			BattleTopnav.SetPaddingFromLeft((int)_switchContainer.Size.X + 10);
			BattleTopnav.ToggleTitle(true);
		} else
		{
			BattleTopnav.SetPaddingFromLeft(0);
			BattleTopnav.ToggleTitle(true);
		}
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
		Nodes.GetRoot().AudioManager.PauseAll();
		Paused = true;
		_pauseOverlay.Visible = true;
	}
	
	private void Resume()
	{
		_pauseOverlay.Visible = false;
		Nodes.GetRoot().SceneManager.ResumeScene();
		Nodes.GetRoot().AudioManager.ResumeAll();
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
		
		_mapGrid = (Vector2I)Map.LocalPosToMilCoords(levelProps.InMapPosition);
		_measureLabel.Text = $"{Map.CurrentLoadedMap.MetersPerCell.X}m";
		_altitiudeLabel.Text = $"{Map.CurrentLoadedMap.GroundLayer.BaseHeight}m";
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
