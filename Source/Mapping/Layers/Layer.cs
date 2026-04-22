using Godot;
using System;
using System.Threading.Tasks;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Layers;

namespace Drumstalotajs.Mapping.Layers;

public abstract partial class Layer<
	[MustBeVariant] TAtlasType,
	[MustBeVariant] TTileInstance,
	[MustBeVariant] TLayerResource
> : BaseLayer
	where TTileInstance : Tile
	where TLayerResource : LayerData
{
	public abstract TAtlasType[] GetAtlas();
	public abstract void AddTile(Vector2I position, TAtlasType atlas);
	public abstract void RemoveTile(Vector2I position);
	public abstract TLayerResource Export();
	public abstract Godot.Collections.Array<TTileInstance> Flash(Vector2I position);
	public abstract Task Load(TLayerResource layerData);
}
