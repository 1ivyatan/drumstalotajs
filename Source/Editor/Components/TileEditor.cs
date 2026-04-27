using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Components.Modals;

namespace Drumstalotajs.Editor.Components;

public partial class TileEditor : Control
{
	[Export] private Map _map;
	[Export] private GroundProps GroundProps;
	
	public override void _Ready()
	{
	}
	
	public void Load(Vector2I coords)
	{
		var tiles = _map.Flash(coords);
		if (tiles.ContainsKey(_map.GroundLayer)) { GroundProps.Load(coords); } else { GroundProps.Close(); }
		GD.Print(tiles);
		Visible = true;
	}
	
	public void Close()
	{
		Visible = false;
	}
}
