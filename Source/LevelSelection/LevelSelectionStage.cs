using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Utils;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Mapping.Tiles.Overlays;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Resources.Progress;

namespace Drumstalotajs.LevelSelection;

public partial class LevelSelectionStage : Node2D
{
	private LevelSet LevelSet { get; set; }
	private LevelProgress LevelProgress { get; set; }
	private Map Map { get; set; }
	private LevelInfoContainer LevelInfoContainer { get; set; }
	private Button start;
	
	public async override void _Ready()
	{
		LevelSet = Nodes.GetRoot().DataManager.LevelSets[0];
		LevelProgress = Nodes.GetRoot().DataManager.LevelSetProgress[LevelSet];
		Map = GetNode("Map") as Map;
		LevelInfoContainer = GetNode("Overlay/LevelInfoContainer") as LevelInfoContainer;
		start = GetNode<Button>("Overlay/Start");

		foreach (var level in LevelSet.Levels)
		{
			LevelMarker tile = await Map.OverlayLayer.AddTile("LevelMarker", level.Position) as LevelMarker;
			tile.Initialize(level);
		}
		
		LevelInfoContainer.SetLevelProgress(this.LevelProgress);
		
		start.Pressed += () => {
			Nodes.GetRoot().SceneManager.Start();
		};

		Map.Selector.Filter = new SelectorFilter([Map.OverlayLayer]);
		
		Map.Selector.ClickedSceneTiles += (FilteredTiles tiles) => {
			if (tiles.ContainsKey(Map.OverlayLayer) && tiles[Map.OverlayLayer].Count > 0)
			{
				var tileProps = (tiles[Map.OverlayLayer][0] as LevelMarker).Props;
				LevelInfoContainer.Open(tileProps, LevelProgress.GetScore(tileProps));
			}
		};

		Map.Selector.ClickedEmpty += () => {
			if (LevelInfoContainer.Visible)
			{
				LevelInfoContainer.Close();
			}
		};
		Map.Mode = MapMode.HiddenInteractable;
	}
}/*return Instances.FirstOrDefault(i => position == LocalToMap(position));*/
