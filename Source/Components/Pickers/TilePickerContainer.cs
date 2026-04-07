using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Components.Pickers;

public partial class TilePickerContainer : Control
{
	[Export] private PackedScene _tilePickerScene;
	
	[Signal] public delegate void SelectedTileEventHandler(SelectedTileData selectedTile);
	
	public void Load(Layer[] layers)
	{
		VBoxContainer container = GetNode<VBoxContainer>("VBoxContainer");
		int count = 0;
		foreach (Layer layer in layers)
		{
			TilePicker picker = _tilePickerScene.Instantiate() as TilePicker;
			picker.SetLayer(layer);
			
			if (layer is SceneLayer)
			{
				foreach (var tile in (layer as SceneLayer).GetAtlas())
				{
					picker.AddItem(tile.Name);
				}
			} else
			{
				foreach (var tile in layer.GetAtlas())
				{
					picker.AddItem(tile.ToString());
				}
			}
			
			picker.SelectedTile += (SelectedTileData selectedTile) => {
				EmitSignal(SignalName.SelectedTile, selectedTile);
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
