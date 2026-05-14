using Godot;
using System;
using System.Linq;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Components;
using Drumstalotajs.Mapping;
using Drumstalotajs.Battle.Components;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Battle.Components;

public partial class Counters : Control
{
	[Export] private Counter _playerDeviceCounter;
	[Export] private Counter _enemyTargetCounter;
	[Export] private Counter _timerCounter;
	[Export] private Counter _turnCounter;
	[Export] private Map _map;
	[Export] private ScoreManager _scoreManager;
	
	public override void _Ready()
	{
		BattleScene scene = Nodes.GetSceneRoot();
		_map.EntityLayer.TileSpawned += IncTileCounters;
		_map.EntityLayer.TileExiting += DecTileCounters;
		_scoreManager.NewTurn += IncTurnCounter;
		_scoreManager.TimeTicked += SetClock;
		_scoreManager.TimeSet += SetClock;
		SetClock(_map.CurrentLoadedMap.TimeLimitSecs);
		_map.EntityLayer.DisabledEntity += (Entity entity) => {
			DecTileCounters(entity);
		};
	}
	
	private void SetClock(double remaining)
	{
		var t = TimeSpan.FromSeconds(remaining);
		_timerCounter.SetText(t.ToString(@"mm\:ss"));
	}
	
	private void IncTurnCounter(int turn)
	{
		_turnCounter.Value = turn;
	}
	
	private void IncTileCounters(SceneTile tile)
	{
		var entity = tile as Entity;
		if (entity is Device device && device.Player == true)
		{
			_playerDeviceCounter.Value += 1;
		} else if (entity.Player == false && entity.Target == true)
		{
			_enemyTargetCounter.Value += 1;
		}
	}
	
	private void DecTileCounters(SceneTile tile)
	{
		var entity = tile as Entity;
		if (entity is Device device && device.Player == true)
		{
			_playerDeviceCounter.Value -= 1;
		} else if (entity.Player == false && entity.Target == true)
		{
			_enemyTargetCounter.Value -= 1;
		}
	}
	
/*	private void UpdateCounters()
	{
		var devs = _map.EntityLayer.Instances
		.Where(e => _map.EntityLayer.GetType(e.TileId) == EntityType.Device)
		.Where(e => ((Entity)e).Player == true);
		_playerDeviceCounter.SetCount(devs.Count());
		
	//	EntityType GetType
	//	_map.EntityLayer.GetType()
		
		//	.Where()
		//var entity = tile as Entity;
		GD.Print(123);
	}*/
}
