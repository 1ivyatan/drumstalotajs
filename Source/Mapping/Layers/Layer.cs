using Godot;
using System;

namespace Drumstalotajs.Mapping.Layers;

public partial class Layer : TileMapLayer, ILayer<Vector2I>
{
	public Vector2I[] GetAtlas()
	{
		TileSet tileSet = TileSet;
		TileSetAtlasSource source = tileSet.GetSource(0) as TileSetAtlasSource;
		int count = source.GetTilesCount();
		Vector2I[] tileAtlas = new Vector2I[count];
		for (int i = 0; i < count; i++)
		{
			Vector2I coords = source.GetTileId(i);
			tileAtlas[i] = coords;
		}
		return tileAtlas;
	}
}
