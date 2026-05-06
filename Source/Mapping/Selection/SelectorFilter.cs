using Godot;
using Godot.Collections;
using System;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Mapping.Selection;

public struct SelectorFilter
{
	public SelectorEntityOwnership EntityOwnership { get; set; } = SelectorEntityOwnership.All;
	public BaseLayer[] Layers { get; set; } = [];
	public FilteredItemIds FilteredItemIds { get; set; } = new();
	
	public SelectorFilter(BaseLayer[] layers)
	{
		Layers = layers;
	}
	
	public SelectorFilter(BaseLayer[] layers,
	FilteredItemIds filteredItemIds,
	SelectorEntityOwnership entityOwnership = SelectorEntityOwnership.All)
	{
		Layers = layers;
		FilteredItemIds = filteredItemIds;
		EntityOwnership = entityOwnership;
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
				var filteredTiles = new Godot.Collections.Array();
				
				if (strict)
				{
					var flashedTile = (layer as SceneLayer).GetInstance(position);
					if (flashedTile != null) filteredTiles = [flashedTile];
				} else
				{
					var flashedTiles = (Godot.Collections.Array)(layer as SceneLayer).Flash(position);
					if (flashedTiles.Count > 0) filteredTiles = flashedTiles;
				}
				
				if (FilteredItemIds.ContainsKey(layer))
				{
					var tileCheck = (Array<int> ids, SceneTile tile) =>
					{
						return ids.Contains(tile.TileId);
					};
					
					foreach (var tile in filteredTiles)
					{
						if (!tileCheck(FilteredItemIds[layer], (SceneTile)tile))
						{
							filteredTiles.Remove(tile);
						}
					}
					
					if (layer is EntityLayer entityLayer &&
					EntityOwnership != SelectorEntityOwnership.All)
					{
						foreach (var tile in filteredTiles)
						{
							if (EntityOwnership == SelectorEntityOwnership.Foreign)
							{
								if (((Entity)tile).Player) filteredTiles.Remove(tile);
							} else if (EntityOwnership == SelectorEntityOwnership.Player)
							{
								if (!((Entity)tile).Player) filteredTiles.Remove(tile);
							}
						}
					}
				}
				
				if (filteredTiles.Count > 0)
				{
					tiles[layer] = filteredTiles;
				}
			}
		}
		
		return tiles;
	}
	
	public static SelectorFilter Empty => new SelectorFilter([], new(), SelectorEntityOwnership.All);
}
