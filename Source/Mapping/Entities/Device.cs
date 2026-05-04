using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Device : Entity
{
	public bool Placed { get; set; } = false;
	
	public override void _Ready()
	{
	}
}
