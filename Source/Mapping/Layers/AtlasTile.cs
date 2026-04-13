using Godot;
using System;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Mapping.Layers;

public partial class AtlasTile : Tile
{
	public TileData TileData { get; protected set; } = null;
	
	public AtlasTile(AtlasLayer layer, Vector2I position)
	{
		if (layer.GetCellAtlasCoords(position) == Types.Vector2I.Negative)
			return;
		
		CellPosition = position;
		TileData = layer.GetCellTileData(position);
	}
	
	public void Export()
	{
		
	}
}
