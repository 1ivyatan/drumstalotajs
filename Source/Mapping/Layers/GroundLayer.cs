using Godot;
using System;

namespace Drumstalotajs.Mapping.Layers;

public partial class GroundLayer : Layer<Vector2I>
{
	public override Vector2I[] GetAtlas()
	{
		TileSet tileSet = TileSet ?? throw new ArgumentNullException(nameof(TileSet));
		TileSetAtlasSource source = tileSet.GetSource(0) as TileSetAtlasSource;
		if (source != null)
		{
			int count = source.GetTilesCount();
			Vector2I[] tileAtlas = new Vector2I[count];
			for (int i = 0; i < count; i++)
			{
				Vector2I coords = source.GetTileId(i);
				tileAtlas[i] = coords;
			}
			return tileAtlas;
		}

		return [];
	}
}
