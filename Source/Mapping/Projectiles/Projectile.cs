using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Mapping.Projectiles;

public partial class Projectile : Node2D
{
	[Signal] public delegate void DetonatedEventHandler();
	
	private Device _device;
	private bool _flying = false;
	
	public override void _Ready()
	{
	}
	
	public void Set(Device device)
	{
		_device = device;
		Position = _device.Position;
	}
	
	public void Launch()
	{
		_device.ExpendShell();
		_flying = true;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		
	}
}
