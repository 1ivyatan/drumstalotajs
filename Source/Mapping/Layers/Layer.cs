using Godot;
using System;

namespace Drumstalotajs.Mapping.Layers;

public abstract partial class Layer<[MustBeVariant] TAtlasType> : TileMapLayer
{
	public int TileSize { get => TileSet.TileSize.X; }
	
	public abstract TAtlasType[] GetAtlas();
	public abstract void AddTile(Vector2I position, TAtlasType atlas);
	public abstract void RemoveTile(Vector2I position);
}
