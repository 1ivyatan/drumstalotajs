using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Components.Pickers;

public partial class TilePickerContainer : Control
{
	[Signal] public delegate void SelectedTileEventHandler(SelectedTileData selectedTile);
	
	public void Load(SceneLayer[] layers)
	{
		int count = 0;
		foreach (SceneLayer layer in layers)
		{
			TilePicker picker = new TilePicker();
			
			foreach (var tile in layer.GetSceneTileAtlas())
			{
				picker.AddItem(tile.Name);
			}
			
			picker.SelectedTile += (SelectedTileData selectedTile) => {
				EmitSignal(SignalName.SelectedTile, selectedTile);
			};
			
			count++;
			
			if (count > 1)
			{ 
				AddChild(new HSeparator());
			}
			
			AddChild(picker);
		}
	}
}
