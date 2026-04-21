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
	
	public FilteredTiles GetTiles(Vector2I position)
	{
		FilteredTiles tiles = new FilteredTiles();
		foreach (var layer in Layers)
		{
			if (layer is AtlasLayer)
			{
				tiles[layer] = (Godot.Collections.Array)(layer as AtlasLayer).Flash(position);
			} else if (layer is SceneLayer)
			{
				tiles[layer] = (Godot.Collections.Array)(layer as SceneLayer).Flash(position);
			}
		}
		return tiles;
	}
	
	public static SelectorFilter Empty => new SelectorFilter([]);
}
