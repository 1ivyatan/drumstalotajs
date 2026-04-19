using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Tiles;

public partial class AtlasTile : Tile
{
	public TileData TileData { get; protected set; } = null;
	
	public AtlasTile(AtlasLayer layer, Vector2I position) : base(position)
	{
		if (layer.GetCellAtlasCoords(position) != Types.Vector2I.Negative)
		{
			TileData = layer.GetCellTileData(position);
		}
	}
}
