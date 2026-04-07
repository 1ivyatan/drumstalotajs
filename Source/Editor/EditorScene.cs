using Godot;
using System;
using Drumstalotajs.Mapping;
using Drumstalotajs.Utils;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Mapping.Tiles.Overlays;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Resources.Progress;
using Drumstalotajs.Components;
using Drumstalotajs.Components.Pickers;

namespace Drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	private Map Map { get; set; }

	private TilePickerContainer _tilePickerContainer;
	
	private Callable _exitPressedCall;
	private Callable _exportPressedCall;
	private Callable _pressedMapCall;
	
	public override void _Ready()
	{
		Map = GetNode("Map") as Map;
		_tilePickerContainer = GetNode("Overlay/TilePickerContainer") as TilePickerContainer;
		Button exit = GetNode<Button>("Overlay/Topnav/Exit");
		Button export = GetNode<Button>("Overlay/Topnav/Export");
		Layer[] sceneLayers = [Map.GroundLayer, Map.OverlayLayer];
		Map.Mode = MapMode.Edit;
		Map.Selector.Filter = new SelectorFilter(sceneLayers);
		_tilePickerContainer.Load(sceneLayers);
		
		_tilePickerContainer.SelectedTile += (SelectedTileData selectedTile) => {
			_selectedTileData = selectedTile;
		};
		
		exit.Pressed += () =>
		{
			Nodes.GetRoot().SceneManager.Start();
		};
		
		export.Pressed += () =>
		{
			Export();
		};
	}
	
	
	private void Export()
	{
		
	}
}
