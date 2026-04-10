using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
using Drumstalotajs.Editor;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs;
using Drumstalotajs.Components.Pickers;

namespace Drumstalotajs.Editor.Components;

public partial class TileSelectionContainer : Control
{
	[Signal] public delegate void SelectedTileEventHandler(PickedTileData pickedTile);
	
	public PickedTileData PickedTileData { get; private set; }
	
	[Export] private PackedScene _layerTilePicker;
	[Export] private Control _container;
	private List<LayerTilePicker> _pickers;
	
	public void Load(LayerBase[] layers)
	{
		var container = GetNode("ScrollContainer/VBoxContainer");
		_pickers = new List<LayerTilePicker>();
		int count = 0;
		foreach (var layer in layers)
		{
			LayerTilePicker picker = _layerTilePicker.Instantiate() as LayerTilePicker;
			picker.Initialize(layer);
			picker.SelectedTile += (PickedTileData pickedTile) => {
				if (PickedTileData != null && pickedTile.Layer != PickedTileData.Layer)
				{
					var oldPicker = _pickers.FirstOrDefault(p => p.Layer == PickedTileData.Layer);
					oldPicker.DeselectAll();
				}
				PickedTileData = pickedTile;
				EmitSignal(SignalName.SelectedTile, pickedTile);
			};
			count++;
			if (count > 1)
			{ 
				container.AddChild(new HSeparator());
			}
			_pickers.Add(picker);
			container.AddChild(picker);
		}
	}
}
