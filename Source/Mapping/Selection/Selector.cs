using Godot;
using System;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Mapping.Tiles.Overlays;

namespace Drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	[Signal] public delegate void HoveredTileEventHandler(Vector2I position);
	[Signal] public delegate void ClickedSceneTilesEventHandler(FilteredTiles tiles);
	[Signal] public delegate void ClickedEmptyEventHandler();
	
	public SelectorFilter Filter { get; set; }
	public SelectorMode Mode { get; set; } = SelectorMode.Locked;
	private Map _map { get; set; }
	
	public override void _Ready()
	{
		_map = GetParent() as Map;
	}
	
	private void Flash(Vector2 localPosition)
	{
		FilteredTiles tiles = new FilteredTiles();
		bool flashed = false;
		foreach (Layer layer in Filter.Layers)
		{
			if (layer is SceneLayer sceneLayer)
			{
				Godot.Collections.Array<SceneTile> flashedTiles = (layer as SceneLayer).Flash(localPosition, 9);
				if (flashedTiles.Count > 0)
				{
					tiles[sceneLayer] = flashedTiles;
					flashed = true;
				}
			}
		}
		
		if (flashed)
		{
			EmitSignal(SignalName.ClickedSceneTiles, tiles);
		} else
		{
			EmitSignal(SignalName.ClickedEmpty);
		}
	}
}
