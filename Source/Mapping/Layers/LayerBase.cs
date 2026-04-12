using Godot;
using System;

namespace Drumstalotajs.Mapping.Layers;

public abstract partial class LayerBase : TileMapLayer
{
	public int TileSize { get => TileSet.TileSize.X; }
}
