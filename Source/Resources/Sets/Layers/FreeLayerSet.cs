using Godot;
using System;

namespace Drumstalotajs.Resources.Sets.Layers;

[GlobalClass]
public abstract partial class FreeLayerSet : Resource
{
	[Export] public int TileSize { get; set; }
}
