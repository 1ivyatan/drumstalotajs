using Godot;
using System;
using Drumstalotajs.Resources.Sets.Layers;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Mapping.Layers;

public abstract partial class FreeLayer : Node2D, ISaveableLayer
{
	[Export] private FreeLayerTileSet TileSet { get; set; }
	[Export] public FreeLayerData LayerData { get; set; }
}
