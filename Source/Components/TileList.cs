using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Components;

public partial class TileList : ItemList
{
	[Signal] public delegate void SelectedAtlasEventHandler(Vector2I atlas);
	[Signal] public delegate void SelectedSceneEventHandler(string name);
	
	private bool isScene = false;
	
	public override void _Ready()
	{
		
	}
	
	public void LoadTiles(BaseLayer layer)
	{
		if (layer is AtlasLayer atlasLayer)
		{
			var atlas = atlasLayer.GetAtlas();
			foreach (var tile in atlas)
			{
				AddItem($"{tile}");
			}
		}
	}
}
