using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Selection;

public struct SelectorFilter
{
	public BaseLayer[] Layers { get; set; }
	
	public SelectorFilter(BaseLayer[] layers)
	{
		Layers = layers;
	}
	
	public FilteredTiles GetTiles(Vector2I position, bool strict = false)
	{
		FilteredTiles tiles = new FilteredTiles();
		foreach (var layer in Layers)
		{
			if (layer is AtlasLayer)
			{
				var flashedTiles = (Godot.Collections.Array)(layer as AtlasLayer).Flash(position);
				if (flashedTiles.Count > 0) tiles[layer] = flashedTiles;
			} else if (layer is SceneLayer)
			{
				if (strict)
				{
					var flashedTile = (layer as SceneLayer).GetInstance(position);
					if (flashedTile != null) tiles[layer] = [flashedTile];
				} else
				{
					var flashedTiles = (Godot.Collections.Array)(layer as SceneLayer).Flash(position);
					if (flashedTiles.Count > 0) tiles[layer] = flashedTiles;
				}
			}
		}
		return tiles;
	}
	
	public static SelectorFilter Empty => new SelectorFilter([]);
}
