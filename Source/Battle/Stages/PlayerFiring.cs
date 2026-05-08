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

namespace Drumstalotajs.Battle.Stages;

public partial class PlayerFiring : Control
{
	private BattleScene _scene;
	private Map _map;
	
	private Dictionary<Device, int> _fireTracker = new();
	
	public override void _Ready()
	{
		_scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
		_map.Selector.Mode = SelectorMode.Locked;
		_scene.BattleTopnav.Title = "Battery!";
		//Fire();
		FireAll();
	}
	
	private void FireAll()
	{
		var devs = _map.EntityLayer.GetPlayerDevices();
		MassFire(devs);
		//if (_map.CurrentLoadedMap.Counterbattery)
		//{
			
		//}
	}
	
	private void MassFire(Device[] devices)
	{
		foreach (var dev in devices)
		{
			if (!_fireTracker.ContainsKey(dev)) _fireTracker[dev] = 0;
			
		}
		
	}
	
	private void Fire()
	{
		var devs = _map.EntityLayer.GetPlayerDevices();
		int firedCount = 0;
		int fireableCount = devs.Where(d => d.Shells > 0).Count();
		
		foreach (var dev in devs)
		{
			SceneTreeTimer delayToFire = GetTree().CreateTimer(GD.RandRange(0.01f, .75f));
			delayToFire.Connect(SceneTreeTimer.SignalName.Timeout , Callable.From(async () => {
				
				
				
				
				
				
				if (dev.Shells > 0)
				{
					var projectile = _map.ProjectileLayer.SpawnProjectile(dev);
					projectile.Connect(Projectile.SignalName.Detonated, Callable.From(() => {
						firedCount++;
						if (firedCount == fireableCount)
						{
							if (_map.CurrentLoadedMap.Counterbattery)
							{
								_scene.StageManager.EnemyFiring();
							} else
							{
								_scene.StageManager.DeviceAdjustment();
							}
						}
					}));
				} else
				{
					dev.CheckAndTryResupply();
				}
			}));
		}
	}
}
