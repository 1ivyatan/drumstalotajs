using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Resources.Mapping.Layers;

[GlobalClass]
public partial class EntityLayerTileData : SceneLayerTileData
{
	[Export] public double Azimuth { get; set; } = 0;
	[Export] public double Integrity { get; set; } = 100;
	[Export] public bool Player { get; set; } = false;
	
	public EntityLayerTileData() : base() {}
	public EntityLayerTileData(EntityLayer layer, Entity tile) : base(layer, tile)
	{
		Azimuth = tile.Azimuth;
		Integrity = tile.Integrity;
		Player = tile.Player;
		Data = tile.Data;
	}
}
