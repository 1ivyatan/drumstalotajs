using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Components;
using Drumstalotajs.Components.Tiling;

namespace Drumstalotajs.Editor.Components;

public partial class InsertWindow : Window
{
	[Signal] public delegate void SelectedTileEventHandler(PickedTileData pickedTile, bool differentLayer);
	
	[Export] private PackedScene _tileListScene;
	[Export] private Container _container;
	[Export] private Button _clearSelection;
	private List<TileList> _pickers;
	public PickedTileData PickedTile { get; private set; } = null;
	
	public override void _Ready()
	{
		_pickers = new List<TileList>();
		_clearSelection.Pressed += () => {
			var oldPicker = _pickers.FirstOrDefault(p => p.Layer == PickedTile.Layer);
			oldPicker.DeselectAll();
			PickedTile = null;
			EmitSignal(SignalName.SelectedTile, PickedTile);
		};
	}
	
	private void SetSelectedTile(PickedTileData data)
	{
		var diffLayer = false;
		if (PickedTile != null && data.Layer != PickedTile.Layer)
		{
			var oldPicker = _pickers.FirstOrDefault(p => p.Layer == PickedTile.Layer);
			oldPicker.DeselectAll();
			diffLayer = true;
		}
		PickedTile = data;
		EmitSignal(SignalName.SelectedTile, PickedTile, diffLayer);
	}
	
	public void LoadTiles(BaseLayer[] layers)
	{
		foreach (var layer in layers)
		{
			var label = new Label();
			label.Text = layer.Name;
			
			var list = _tileListScene.Instantiate() as TileList;
			list.LoadTiles(layer);
			list.SelectedAtlas += SetSelectedTile;
			_pickers.Add(list);
			
			_container.AddChild(label);
			_container.AddChild(list);
			if (layers.Length > 1)
			{
				_container.AddChild(new HSeparator());
			}
		}
	}
}
