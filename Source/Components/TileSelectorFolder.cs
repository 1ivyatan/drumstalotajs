using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Components;

public partial class TileSelectorFolder : Control
{
	[Signal] public delegate void SelectedTileEventHandler(SceneLayer layer, string name);
	
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
			
			picker.SelectedTile += (SceneLayer layer, string name) => {
				EmitSignal(SignalName.SelectedTile, layer, name);
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
