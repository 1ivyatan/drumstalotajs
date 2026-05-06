using Godot;
using System;
using System.Linq;
using Drumstalotajs;
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
	[Export] private Map _map;
	[Export] private ScoreManager _scoreManager;
	
	public override void _Ready()
	{
		_map.EntityLayer.TileSpawned += IncCounters;
		_map.EntityLayer.TileExiting += DecCounters;
	}
	
	private void IncCounters(SceneTile tile)
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
	
	private void DecCounters(SceneTile tile)
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
