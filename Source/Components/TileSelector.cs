using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Components;

public partial class TileSelector : ItemList
{
	public override void _Ready()
	{
		
	}
	
	/*
	public void Load(SceneLayer[] layers)
	{
		int count = 0;
		foreach (SceneLayer layer in layers)
		{
		//	ItemList container = new ItemList();
		
		//	foreach (var tile in layer.GetSceneTileAtlas())
		//	{
		//		container.AddItem(tile.Name);
		//	}
			
		//		container.AddItem("Kujoe");
			
			count++;
			
		//	if (count > 1)
		//	{ 
				AddChild(new HSeparator());
				AddChild(new HSeparator());
				AddChild(new HSeparator());
		//	}
			
		//	AddChild(container);
		}
	}*/
}
