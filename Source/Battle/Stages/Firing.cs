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
	
	private Device[] _playerDevs = null;
	private int _firedPlayerDeviceCount = 0;
	private int _totalPlayerDeviceCount = 0;
	
	private Device[] _enemyDevs = null;
	private int _firedEnemyDeviceCount = 0;
	private int _totalEnemyDeviceCount = 0;
	
	private FiringMode _mode;
	
	public override void _Ready()
	{
		_scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
		_map.Mode = MapMode.HiddenInteractable;
		_scene.ScoreManager.CheckAndActivate();
		_scene.BattleTopnav.Title = "";
	}
	
	public void StartFiring(FiringMode mode)
	{
		_mode = mode;
		
		_playerDevs = _map.EntityLayer.GetPlayerDevices();//.Where(d => d.Shells > 0).ToArray();
		_totalPlayerDeviceCount = _playerDevs.Length;
		_firedPlayerDeviceCount = 0;
		
		_enemyDevs = _map.EntityLayer.GetEnemyDevices();//.Where(d => d.Shells > 0).ToArray();
		_totalEnemyDeviceCount = _enemyDevs.Length;
		_firedEnemyDeviceCount = 0;
		
		switch (mode)
		{
			case FiringMode.Player:
				FirePlayerDevices();
				break;
			case FiringMode.Enemy:
				FireEnemyDevices();
				break;
			case FiringMode.Both:
				FireAll();
				break;
			default: break;
		}
	}
	
	private void FireAll()
	{
		FirePlayerDevices();
	}
	
	private void FireEnemyDevices()
	{
		if (_map.CurrentLoadedMap.Counterbattery && _mode != FiringMode.Player)
		{
			_scene.BattleTopnav.Title = "Enemy Counterbattery!";
			MassFire(_enemyDevs);
		} else
		{
			NextStage();
		}
	}
	
	private void FirePlayerDevices()
	{
		_scene.BattleTopnav.Title = "Battery!";
		MassFire(_playerDevs);
	}
	
	private void NextStage()
	{
		if (_scene.ScoreManager.CanContinue())
		{
			_scene.StageManager.DeviceAdjustment();
		} else
		{
			_scene.StageManager.End();
		}
	}
	
	private void MassFire(Device[] devices)
	{
		foreach (var dev in devices)
		{
			_fireTracker[dev] = 0;
			SceneTreeTimer delayToFire = GetTree().CreateTimer(GD.RandRange(0.01f, .5f), false);
			delayToFire.Connect(SceneTreeTimer.SignalName.Timeout , Callable.From(() => {
				BatchFire(dev);
			}));
		}
	}
	
	private async void BatchFire(Device device)
	{
		int expendable = device.Shells < device.ShellsPerTurn
			? device.Shells
			: device.ShellsPerTurn;
				
		if (device.Shells > 0)
		{
			for (int i = 0; i < expendable; i++)
			{
				var projectile = _map.ProjectileLayer.SpawnProjectile(device);
				projectile.Connect(Projectile.SignalName.Detonated, Callable.From(
					(Device device) => {
						LogTracker(device, 1);
					}
				));
				await ToSignal(GetTree().CreateTimer(
					((DevicePropertiesData)device.Properties).DelayBetweenFires, false
				), SceneTreeTimer.SignalName.Timeout);
			}
		} else
		{
			if (device.Player && _map.CurrentLoadedMap.PlayerResupply)
			{
				device.CheckAndTryResupply();
			} else if (!device.Player && _map.CurrentLoadedMap.EnemyResupply)
			{
				device.CheckAndTryResupply();
			}
			LogTracker(device, expendable);
		}
	}
	
	private void LogTracker(Device device, int inc)
	{
		int expendable = device.Shells < device.ShellsPerTurn
			? device.Shells
			: device.ShellsPerTurn;
			
		_fireTracker[device] += inc;
		if (_fireTracker[device] == expendable)
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
}
