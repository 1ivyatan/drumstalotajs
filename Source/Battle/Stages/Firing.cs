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

public partial class Firing : Control
{
	private BattleScene _scene;
	private Map _map;
	
	private Dictionary<Device, int> _fireTracker = new();
	private int _firedPlayerDeviceCount = 0;
	private int _firedEnemyDeviceCount = 0;
	private int _totalPlayerDeviceCount = 0;
	private int _totalEnemyDeviceCount = 0;
	private Device[] _playerDevs = null;
	private Device[] _enemyDevs = null;
	
	public override void _Ready()
	{
		_scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
		_map.Mode = MapMode.HiddenInteractable;
		_scene.BattleTopnav.Title = "Battery!";
		FireAll();
	}
	
	private void FireAll()
	{
		_playerDevs = _map.EntityLayer.GetPlayerDevices().Where(d => d.Shells > 0).ToArray();
		Device[] enemyDevs = null;
		
		_firedPlayerDeviceCount = 0;
		_firedEnemyDeviceCount = 0;
		_totalPlayerDeviceCount = _playerDevs.Length;
		
		if (_map.CurrentLoadedMap.Counterbattery)
		{
			_enemyDevs = _map.EntityLayer.GetEnemyDevices().Where(d => d.Shells > 0).ToArray();
			_totalEnemyDeviceCount = enemyDevs.Length;
		}
		
		FirePlayerDevices();
	}
	
	private void FireEnemyDevices()
	{
		if (_map.CurrentLoadedMap.Counterbattery)
		{
			_scene.BattleTopnav.Title = "Enemy Counterbattery!";
			MassFire(_enemyDevs);
		} else
		{
			NextStage();
		}
	}
	
	private void NextStage()
	{
		/* here be score checking */
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
