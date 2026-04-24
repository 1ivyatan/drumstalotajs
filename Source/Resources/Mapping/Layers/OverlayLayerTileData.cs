using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Resources.Mapping.Layers;

[GlobalClass]
public partial class OverlayLayerTileData : SceneLayerTileData
{
	[Export] public double Radians { get; set; }
	
	public OverlayLayerTileData() : base() {}
	public OverlayLayerTileData(OverlayLayer layer, OverlayTile tile) : base(layer, tile)
	{
		Radians = tile.Rotation;
		Data = tile.Data;
	}
}
