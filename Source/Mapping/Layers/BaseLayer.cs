using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Mapping.Layers;

public abstract partial class BaseLayer : TileMapLayer
{
	[Signal] public delegate void ChangedLayerEventHandler();
	public int TileSize { get => TileSet.TileSize.X; }
}
