using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Components;

namespace Drumstalotajs.Editor.Components;

public partial class InsertWindow : Window
{
	[Export] private PackedScene _tileListScene;
	[Export] private Container _container;
	[Export] private Button _clearSelection;
	
	public override void _Ready()
	{
		
	}
	
	public void LoadTiles(BaseLayer[] layers)
	{
		foreach (var layer in layers)
		{
			var label = new Label();
			label.Text = layer.Name;
			
			var list = _tileListScene.Instantiate() as TileList;
			list.LoadTiles(layer);
			
			_container.AddChild(label);
			_container.AddChild(list);
			if (layers.Length > 1)
			{
				_container.AddChild(new HSeparator());
			}
		}
	}
}
