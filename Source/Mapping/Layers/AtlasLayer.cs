using Godot;
using System;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Mapping.Layers;

public partial class AtlasLayer : Layer<Vector2I>
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
	
	public override void AddTile(Vector2I position, Vector2I atlas)
	{
		SetCell(position, 0, atlas, 0);
	}
	
	public override void RemoveTile(Vector2I position)
	{
		EraseCell(position);
	}
	
	public override Godot.Collections.Array<Tile> Flash(Vector2I position)
	{
		if (GetCellAtlasCoords(position) == Types.Vector2I.Negative) return [];
		
		return [ new AtlasTile(this, position) ];
	}
}
