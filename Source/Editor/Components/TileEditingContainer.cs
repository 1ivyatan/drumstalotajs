using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Editor;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Editor.Components;

public partial class TileEditingContainer : Control
{
	[Export] private Map _map;
	[Export] private GroundProperties _groundProperties;
	
	public override void _Ready()
	{
		
	}
	
	public void Load(Vector2I position)
	{
		FilteredTiles tiles = _map.Selector.GetTiles(position);
		if (tiles.Count > 0)
		{
			bool hasGround = false;
			foreach (var layerTiles in tiles)
			{
				if (layerTiles.Value.Count == 0) continue;
				
				if (layerTiles.Key is GroundLayer)
				{
					_groundProperties.Load((GroundTile)layerTiles.Value[0]);
					hasGround = true;
				}
			}
			
			if (!hasGround) _groundProperties.Close();
			
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
