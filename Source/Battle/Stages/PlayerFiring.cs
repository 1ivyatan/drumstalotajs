using Godot;
using Godot.Collections;
using System;
using System.Linq;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Battle;
using Drumstalotajs.Mapping;
using Drumstalotajs.Battle.Components;
using Drumstalotajs.Resources.Mapping.Sets;
using Drumstalotajs.Mapping.Entities;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Mapping.Overlays;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;
using System.Threading.Tasks;
using Drumstalotajs.Components;
using Drumstalotajs.Mapping.Projectiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Battle.Stages;

public partial class PlayerFiring : Control
{
	private BattleScene _scene;
	private Map _map;
	
	private Dictionary<Device, int> _fireTracker = new();
	private int _firedPlayerDeviceCount = 0;
	private int _firedEnemyDeviceCount = 0;
	private int _totalPlayerDeviceCount = 0;
	private int _totalEnemyDeviceCount = 0;
	
	public override void _Ready()
	{
		_scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
		_map.Selector.Mode = SelectorMode.Locked;
		_scene.BattleTopnav.Title = "Battery!";
		FireAll();
	}
	
	private void FireAll()
	{
		Device[] playerDevs = _map.EntityLayer.GetPlayerDevices();
		Device[] enemyDevs = null;
		
		_firedPlayerDeviceCount = 0;
		_firedEnemyDeviceCount = 0;
		_totalPlayerDeviceCount = playerDevs.Length;
		
		if (_map.CurrentLoadedMap.Counterbattery)
		{
			enemyDevs = _map.EntityLayer.GetEnemyDevices();
			_totalEnemyDeviceCount = enemyDevs.Length;
		}
		
		FirePlayerDevices();
	}
	
	private void FireEnemyDevices()
	{
		if (_map.CurrentLoadedMap.Counterbattery)
		{
		_scene.BattleTopnav.Title = "Enemy Counterbattery!";
			Device[] enemyDevs = _map.EntityLayer.GetEnemyDevices();
			MassFire(enemyDevs);
		} else
		{
			NextStage();
		}
	}
	
	private void NextStage()
	{
		_scene.StageManager.DeviceAdjustment();
	}
	
	private void FirePlayerDevices()
	{
		Device[] playerDevs = _map.EntityLayer.GetPlayerDevices();
		MassFire(playerDevs);
	}
	
	private void MassFire(Device[] devices)
	{
		foreach (var dev in devices)
		{
			_fireTracker[dev] = 0;
			SceneTreeTimer delayToFire = GetTree().CreateTimer(GD.RandRange(0.01f, .5f));
			delayToFire.Connect(SceneTreeTimer.SignalName.Timeout , Callable.From(() => {
				BatchFire(dev);
			}));
		}
	}
	
	private async void BatchFire(Device device)
	{
		if (device.Shells > 0)
		{
			for (int i = 0; i < device.ShellsPerTurn; i++)
			{
				var projectile = _map.ProjectileLayer.SpawnProjectile(device);
				projectile.Connect(Projectile.SignalName.Detonated, Callable.From(
					(Device device) => {
						_fireTracker[device]++;
						if (_fireTracker[device] == device.ShellsPerTurn)
						{
							if (device.Player)
							{
								_firedPlayerDeviceCount++;
								if (_firedPlayerDeviceCount == _totalPlayerDeviceCount)
								{
									FireEnemyDevices();
								}
							} else
							{
								_firedEnemyDeviceCount++;
								if (_firedEnemyDeviceCount == _totalEnemyDeviceCount)
								{
									NextStage();
								}
							}
						}
					}
				));
				await ToSignal(GetTree().CreateTimer(
					((DevicePropertiesData)device.Properties).DelayBetweenFires
				), SceneTreeTimer.SignalName.Timeout);
			}
		} else
		{
			device.CheckAndTryResupply();
		}
	}
}
