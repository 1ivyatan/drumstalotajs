using Godot;
using System;
using System.Linq;
using System.Collections; 
using System.Collections.Generic;
using System.Collections.Specialized;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Resources.Sets.LevelSets;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.LevelSelection;

public partial class LevelSelectionScene : Node
{
	private LevelSet LevelSet { get; set; }
	private Map Map { get; set; }
	private Button _toStart;
	
	
	public override void _Ready()
	{
		Map = GetNode("Map") as Map;
		Node overlay = GetNode("Overlay");
		LevelSet = Nodes.GetRoot().DataManager.LevelSetProgress.Keys.First();
		Map.LoadMap(LevelSet.BackgroundMapMeta);
		
		foreach (var level in LevelSet.)
		
		Map.OverlayLayer.ClickedTile += (Vector2 position, FreeTile tile) =>
		{
			GD.Print(tile);
		};
		
		Map.OverlayLayer.ClickedEmptyTile += (Vector2 position) =>
		{
			GD.Print(tile);
		};
		
		_toStart = overlay.GetNode<Button>("ToStart");
		_toStart.Pressed += () => 
		{
			Nodes.GetRoot().SceneManager.Start();
		};
	}
}
