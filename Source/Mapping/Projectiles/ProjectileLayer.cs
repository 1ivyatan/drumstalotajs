using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Mapping.Projectiles;

public partial class ProjectileLayer : Node2D
{
	public int MaxProjectileCount { get; } = Constants.Mapping.MaxProjectileCount;
}
