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
	
	public EntityLayerDeviceTileData() : base() {}
	public EntityLayerDeviceTileData(EntityLayer layer, Device device) : base(layer, device)
	{
		Angle = device.Angle;
	}
}
