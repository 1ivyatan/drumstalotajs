using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Layers;

namespace Drumstalotajs.Mapping.Projectiles;

public partial class Projectile : Node2D
{
	[Signal] public delegate void DetonatedEventHandler();
	
	public override void _Ready()
	{
	}
}
