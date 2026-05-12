using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Resources.Mapping;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources.Saves;

namespace Drumstalotajs.Battle.Components;

public partial class ScoreManager : Node
{
	[Signal] public delegate void TimeTickedEventHandler(double remaining);
	[Signal] public delegate void TimeSetEventHandler(double remaining);
	[Signal] public delegate void NewTurnEventHandler(int turn);
	
	[Export] private Map _map;
	
	public double TimeLimit { get; private set; } = 0;
	public double RemainingTime { get; private set; } = 0;
	public int Turn { get; private set; } = 1;
	
	private double _elapsed { get; set; } = 0;
	private bool _running { get; set; } = false;
	public LevelProps LevelProps { get; private set; } = null;
	public LevelSet LevelSet { get; private set; } = null;
	
	public override void _Ready()
	{
	}

	public void NextTurn()
	{
		Turn++;
		EmitSignal(SignalName.NewTurn, Turn);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if (_running)
		{
			_elapsed += delta;
			if (_elapsed >= 1)
			{
				_elapsed -= 1;
				if (RemainingTime-- <= 0)
				{
					_running = false;
					RemainingTime = 0;
				}
				EmitSignal(SignalName.TimeTicked, RemainingTime);
			}
		}
	}
	
	public bool HasVictory()
	{
		var enemyCount = _map.EntityLayer.GetEnemyTargets().Length;
		return enemyCount == 0;
	}
	
	public void CheckAndActivate()
	{
		if (!_running && RemainingTime > 0)
		{
			SetRunning(true);
		}
	}
	
	public void SetRunning(bool running)
	{
		_running = running;
	}
	
	public bool CanContinue()
	{
		return RemainingTime > 0 && !HasVictory();
	}
	
	public void PrepareScoring(MapResource mapResource)
	{
		TimeLimit = mapResource.TimeLimitSecs;
		RemainingTime = TimeLimit;
		EmitSignal(SignalName.TimeSet, RemainingTime);
	}
	
	public void PrepareScoring(MapResource mapResource, LevelSet levelSet, LevelProps levelProps)
	{
		LevelSet = levelSet;
		LevelProps = levelProps;
		PrepareScoring(mapResource);
	}
	
	public bool IsInLevel()
	{
		return LevelSet != null && LevelProps != null;
	}
	
	public void RecordScore()
	{
		if (LevelSet != null && LevelProps != null)
		{
			var saveManager = Nodes.GetRoot().SaveManager;
			var score = new LevelScore(LevelProps);
			saveManager.AddScore(LevelSet, score);
		}
	}
}
