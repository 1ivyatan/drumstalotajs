using Godot;
using System;
using System.Linq;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Components.Tiling;

public partial class TileList : ItemList
{
	[Signal] public delegate void SelectedAtlasEventHandler(PickedTileData data);
	[Export] private int AtlasColumnCount = 6;
	[Export] private int SceneColumnCount = 3;
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
				MaxColumns = AtlasColumnCount;
				Texture2D texture = Utilities.Layers.GetCellTexture(layer, tile);
				AddItem($"{tile}", texture);
			}
		} else if (layer is SceneLayer sceneLayer)
		{
			var atlas = sceneLayer.GetAtlas();
			foreach (var tile in atlas)
			{
				MaxColumns = SceneColumnCount;
				Texture2D texture = sceneLayer.GetAtlasData(tile).Thumbnail;
				AddItem($"{tile}", texture);
			}
		}
		Layer = layer;
	}
}
