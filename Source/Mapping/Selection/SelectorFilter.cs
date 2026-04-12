using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Selection;

public struct SelectorFilter
{
	public LayerBase[] Layers { get; set; }
	
	public SelectorFilter(LayerBase[] layers)
	{
		Layers = layers;
	}
	
	public FilteredTiles GetTiles(Vector2I position)
	{
		FilteredTiles tiles = new FilteredTiles();
		foreach (var layer in Layers)
		{
			if (layer is AtlasLayer)
			{
				tiles[layer] = (Godot.Collections.Array)(layer as AtlasLayer).Flash(position);
			}
		}
		return tiles;
	}
}
