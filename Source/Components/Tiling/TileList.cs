using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Components.Tiling;

public partial class TileList : ItemList
{
	[Signal] public delegate void SelectedAtlasEventHandler(PickedTileData data);
	public BaseLayer Layer { get; private set; } = null;
	
	public override void _Ready()
	{
		ItemSelected += (long index) => {
			if (Layer != null)
			{
				string name = GetItemText((int)index);
				EmitSignal(SignalName.SelectedAtlas, new PickedTileData(Layer, name));
			}
		};
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
		} else if (layer is SceneLayer sceneLayer)
		{
			var atlas = sceneLayer.GetAtlas();
			foreach (var tile in atlas)
			{
				AddItem($"{tile}");
			}
		}
		Layer = layer;
	}
}
