using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Device : Entity
{
	public double Angle { get; set; } = -1;
	public double Traverse { get; set; } = 0;
	
	public override void _Ready()
	{
	}
}
