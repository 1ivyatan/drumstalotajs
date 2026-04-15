using Godot;
using System;

namespace Drumstalotajs.Mapping.Layers;

public abstract partial class LayerBase : TileMapLayer
{
	[Signal] public delegate void ChangedLayerEventHandler();
	public int TileSize { get => TileSet.TileSize.X; }
}
