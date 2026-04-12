using Godot;
using System;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	[Export] private Map _map;
	
	public SelectorFilter Filter { get; set; }
	
	public FilteredTiles GetTiles(Vector2I position)
	{
		FilteredTiles tiles = new FilteredTiles();
		
		if (_map.GroundLayer.GetCellAtlasCoords(position) != Types.Vector2I.Negative)
		{
			tiles = Filter.GetTiles(position);
		}
		
		return tiles;
	}
}
