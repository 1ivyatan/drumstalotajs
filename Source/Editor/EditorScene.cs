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

namespace Drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	private Map Map { get; set; }

	private TileSelectorFolder _tileSelectorFolder;
	
	private Callable _exitPressedCall;
	private Callable _exportPressedCall;
	private Callable _pressedMapCall;
	
	public override void _Ready()
	{
		Map = GetNode("Map") as Map;
		_tileSelectorFolder = GetNode("Overlay/TileSelectorFolder") as TileSelectorFolder;
		Button exit = GetNode<Button>("Overlay/Topnav/Exit");
		Button export = GetNode<Button>("Overlay/Topnav/Export");
		SceneLayer[] sceneLayers = [Map.OverlayLayer];
		Map.Mode = MapMode.Edit;
		Map.Selector.Filter = new SelectorFilter(sceneLayers);
		_tileSelectorFolder.Load(sceneLayers);
		
		_tileSelectorFolder.SelectedTile += (string name) => {
			GD.Print(name);
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
