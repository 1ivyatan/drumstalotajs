using Godot;
using System;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Mapping.Projectiles;

public partial class ProjectileLayer : Node2D
{
	[Export] private Map _map;
	public int MaxProjectileCount { get; } = Constants.Mapping.MaxProjectileCount;
	[Export] private PackedScene _projectileScene;
	private List<Projectile> _projectiles = new();
	
	public Projectile SpawnProjectile(Device device)
	{
		if (_projectiles.Count >= MaxProjectileCount)
		{
			var oldestProjectile = _projectiles[0];
			oldestProjectile.QueueFree();
			RemoveChild(oldestProjectile);
		}
		
		var projectile = _projectileScene.Instantiate() as Projectile;
		projectile.Set(device, _map);
		AddChild(projectile);
		projectile.Launch();
		return projectile;
	}
	
	public void ClearProjectiles()
	{
		foreach (var projectile in _projectiles)
		{
			projectile.QueueFree();
			RemoveChild(projectile);
		}
	}
}
