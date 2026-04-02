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
	private LevelInfoContainer LevelInfoContainer { get; set; }
	private Button start;
	
	public async override void _Ready()
	{
		LevelSet = Nodes.GetRoot().DataManager.LevelSets[0];
		Map = GetNode("Map") as Map;
		LevelInfoContainer = GetNode("Overlay/LevelInfoContainer") as LevelInfoContainer;
		start = GetNode<Button>("Overlay/Start");

		foreach (var level in LevelSet.Levels)
		{
			LevelMarker tile = await Map.OverlayLayer.AddTile("LevelMarker", level.Position) as LevelMarker;
			tile.Initialize(level);
		}
		
		start.Pressed += () => {
			Nodes.GetRoot().SceneManager.Start();
		};

		Map.Selector.Filter = new SelectorFilter([Map.OverlayLayer]);
		Map.Selector.PressedOverlay += (OverlayTile tile) => {
			LevelInfoContainer.Open((tile as LevelMarker).Props);
		};

		Map.Selector.PressedEmpty += () => {
			if (LevelInfoContainer.Visible)
			{
				LevelInfoContainer.Close();
			}
		};
		Map.Mode = MapMode.HiddenInteractable;
	}
}/*return Instances.FirstOrDefault(i => position == LocalToMap(position));*/
