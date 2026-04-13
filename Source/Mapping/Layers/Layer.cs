using Godot;
using System;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Mapping.Layers;

public abstract partial class Layer<[MustBeVariant] TAtlasType, [MustBeVariant] TTile, [MustBeVariant] TLayerResource> : LayerBase where TTile : Tile where TLayerResource : LayerData
{
	public abstract TAtlasType[] GetAtlas();
	public abstract void AddTile(Vector2I position, TAtlasType atlas);
	public abstract void RemoveTile(Vector2I position);
	public abstract Godot.Collections.Array<TTile> Flash(Vector2I position);
	public abstract TLayerResource Axport();
	public abstract void Export();
}
