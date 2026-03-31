using Godot;
using System;

namespace Drumstalotajs.Resources.Sets.Layers;

[GlobalClass]
public partial class FreeLayerTileSet : Resource
{
	[Export] public int TileSize { get; set; }
	
}
