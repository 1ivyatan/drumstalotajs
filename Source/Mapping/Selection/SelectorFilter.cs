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
			tiles[layer] = layer.Flash(position);
		}
		return tiles;
	}
}
