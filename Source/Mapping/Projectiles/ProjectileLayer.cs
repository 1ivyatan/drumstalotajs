using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Mapping.Projectiles;

public partial class ProjectileLayer : Node2D
{
	public int MaxProjectileCount { get; } = Constants.Mapping.MaxProjectileCount;
	
	public Projectile SpawnProjectile(Device device)
	{
		return null;
	}
	
	public void ClearProjectiles() {}
}
