using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Components;

public partial class TileSelectorFolder : Control
{
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
			
			count++;
			
			if (count > 1)
			{ 
				AddChild(new HSeparator());
			}
			
			AddChild(picker);
		}
	}
}
