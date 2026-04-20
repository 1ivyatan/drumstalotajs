using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Tiles;

public partial class AtlasTile : Tile
{
	public TileData TileData { get; protected set; } = null;
	public Vector2I CellPosition { get; protected set; }
	
	public AtlasTile(AtlasLayer layer, Vector2I position)
	{
		CellPosition = position;
		
		if (layer.GetCellAtlasCoords(position) != Constants.Vector2I.Negative)
		{
			TileData = layer.GetCellTileData(position);
		}
	}
}
