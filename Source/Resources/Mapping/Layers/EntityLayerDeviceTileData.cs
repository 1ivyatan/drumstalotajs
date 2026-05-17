using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Resources.Mapping.Layers;

[GlobalClass]
public partial class EntityLayerDeviceTileData : EntityLayerTileData
{
	[Export] public double Angle { get; set; } = -1;
	[Export] public double Traverse { get; set; } = 0;
	[Export] public int ShellsPerTurn { get; set; } = 1;
	[Export] public int Shells { get; set; } = -1;
	
	public EntityLayerDeviceTileData() : base() {}
	public EntityLayerDeviceTileData(EntityLayer layer, Device device) : base(layer, device)
	{
		Angle = device.Angle;
		ShellsPerTurn = device.ShellsPerTurn;
		Traverse = device.Traverse;
		Shells = device.Shells;
	}
}
