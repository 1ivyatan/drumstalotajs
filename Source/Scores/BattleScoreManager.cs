using Godot;
using System;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Scores;

public partial class BattleScoreManager : Node
{
	[Signal] public delegate void TickedEventHandler(double remaining);
	
	public BattleScoreState State { get; private set; } = BattleScoreState.Empty;
	private double TimeLimit { get; set; }
	
	private int _lastRemainingSecond = -1;
	private Timer _timer;
	private Map _map;
	
	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if (State == BattleScoreState.Running)
		{
			int currentSecond = Mathf.CeilToInt(_timer.TimeLeft);
			if (currentSecond != _lastRemainingSecond)
			{
				_lastRemainingSecond = currentSecond;
				Tick();
			}
		}
	}
	
	public double GetRemainingTime()
	{
		return TimeLimit - ( _lastRemainingSecond >= 0 ? _lastRemainingSecond : 0);
	}
	
	private void Tick()
	{
		EmitSignal(SignalName.Ticked, GetRemainingTime());
	}
	
	public void Reset()
	{
		_lastRemainingSecond = Mathf.CeilToInt(_timer.TimeLeft);
		Tick();
		State = BattleScoreState.Loaded;
	}
	
	public void Pause()
	{
		if (State == BattleScoreState.Loaded) return;
		State = BattleScoreState.Paused;
	}
	
	public void Resume()
	{
		if (State == BattleScoreState.Loaded) return;
		State = BattleScoreState.Running;
	}
	
	public void Start()
	{
		State = BattleScoreState.Running;
	}
	
	public void Load(Map map)
	{
		_map = map;
		var mapData = _map.MapData;
		TimeLimit = mapData.TimeLimit;
		State = BattleScoreState.Loaded;
	}
}
