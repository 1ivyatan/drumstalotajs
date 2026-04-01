using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Utils;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Mapping.Tiles.Overlays;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.LevelSelection;

public partial class LevelSelectionStage : Node2D
{
	private LevelSet LevelSet { get; set; }
	private Map Map { get; set; }
	
	public async override void _Ready()
	{
		LevelSet = Nodes.GetRoot().DataManager.LevelSets[0];
		Map = GetNode("Map") as Map;

		foreach (var level in LevelSet.Levels)
		{
			LevelMarker tile = await Map.OverlayLayer.AddTile("LevelMarker", level.Position) as LevelMarker;
			//tile.Initialize();
			GD.Print(tile);
		}

		Map.Selector.Filter = new SelectorFilter([Map.OverlayLayer]);
		Map.Selector.PressedOverlay += (OverlayTile tile) => {
			GD.Print(tile);
		};
		Map.Mode = MapMode.HiddenInteractable;
	}
}/*return Instances.FirstOrDefault(i => position == LocalToMap(position));*/
