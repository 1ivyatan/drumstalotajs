using Godot;
using System;
using Drumstalotajs.Editor;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs;
using Drumstalotajs.Components.Pickers;

namespace Drumstalotajs.Editor.Components;

public partial class TileSelectionContainer : Control
{
	[Export] private PackedScene _layerTilePicker;
	[Export] private Control _container;
	
	public void Load(LayerBase[] layers)
	{
		var container = GetNode("ScrollContainer/VBoxContainer");
		int count = 0;
		foreach (var layer in layers)
		{
			LayerTilePicker picker = _layerTilePicker.Instantiate() as LayerTilePicker;
			picker.Initialize(layer);
			picker.SelectedTile += (PickedTileData pickedTile) => {
				GD.Print(pickedTile.Layer);
			};
			count++;
			if (count > 1)
			{ 
				container.AddChild(new HSeparator());
			}
			container.AddChild(picker);
		}
	}
}
