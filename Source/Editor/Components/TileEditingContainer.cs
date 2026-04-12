using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Editor;
using Drumstalotajs.Mapping;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Editor.Components;

public partial class TileEditingContainer : Control
{
	[Export] private Map _map;
	
	public override void _Ready()
	{
		
	}
	
	public void Load(Vector2I position)
	{
		FilteredTiles tiles = _map.Selector.GetTiles(position);
		if (tiles.Count > 0)
		{
			Visible = true;
		} else
		{
			Close();
		}
	}
	
	public void Close()
	{
		Visible = false;
	}
}
