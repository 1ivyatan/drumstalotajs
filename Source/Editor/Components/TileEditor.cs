using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Editor.Components;

public partial class TileEditor : Control
{
	[Export] private Map _map;
	[Export] private GroundProps GroundProps;
	[Export] private DecorationProps DecorationProps;
	
	public override void _Ready()
	{
	}
	
	public void Load(Vector2I coords)
	{
		var tiles = _map.Flash(coords);
		
		if (tiles.ContainsKey(_map.GroundLayer)) {
			GroundProps.Load((Tile)tiles[_map.GroundLayer][0]);
		} else { GroundProps.Close(); }
		
		if (tiles.ContainsKey(_map.DecorationLayer)) {
			DecorationProps.Load((Tile)tiles[_map.DecorationLayer][0]);
		} else { DecorationProps.Close(); }

		Visible = true;
	}
	
	public void Close()
	{
		Visible = false;
	}
}
