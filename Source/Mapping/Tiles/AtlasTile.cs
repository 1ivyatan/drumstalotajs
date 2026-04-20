using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Tiles;

public partial class AtlasTile : Tile
{
	public TileData TileData { get; protected set; } = null;
	public Vector2I CellPosition { get; protected set; }
	
	public AtlasTile(AtlasLayer layer, Vector2I position)
	{
		CellPosition = position;
		
		if (layer.GetCellAtlasCoords(position) != Types.Vector2I.Negative)
		{
			TileData = layer.GetCellTileData(position);
		}
	}
}
