using Godot;
using System;
using Drumstalotajs.Resources.Sets.Layers;

namespace Drumstalotajs.Mapping.Layers;

public abstract partial class FreeLayer : Node2D, ISaveableLayer
{
	[Export] private FreeLayerTileSet FreeTileSet { get; set; }
}
