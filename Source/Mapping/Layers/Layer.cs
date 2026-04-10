using Godot;
using System;

namespace Drumstalotajs.Mapping.Layers;

public abstract partial class Layer<[MustBeVariant] TAtlasType> : TileMapLayer
{
	public abstract TAtlasType[] GetAtlas();
	
	
}
