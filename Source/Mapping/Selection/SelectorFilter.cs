using Godot;
using Godot.Collections;
using System;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Mapping.Selection;

public struct SelectorFilter
{
	public BaseLayer[] Layers { get; set; } = [];
	public FilteredItemIds FilteredItemIds { get; set; } = new();
	
	public SelectorFilter(BaseLayer[] layers)
	{
		Layers = layers;
	}
	
	public SelectorFilter(BaseLayer[] layers, FilteredItemIds filteredItemIds)
	{
		Layers = layers;
		FilteredItemIds = filteredItemIds;
	}
	
	//public SelectorFilter(BaseLayer[] layers)
	//{
//		Layers = layers;
	//}
	
	/*
	
namespace Drumstalotajs.Mapping.Tiles;

public partial class SceneTile : Tile
	*/
	
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
		
		if (FilteredItemIds.Count > 0)
		{
			//foreach (var tile in tiles)
			//{
				GD.Print(FilteredItemIds);
			//}
		}
		
		return tiles;
	}
	
	public static SelectorFilter Empty => new SelectorFilter([], new());
}
