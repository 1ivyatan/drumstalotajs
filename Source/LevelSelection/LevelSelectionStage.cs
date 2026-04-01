using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Utils;
using Drumstalotajs.Resources.Levels;

namespace Drumstalotajs.LevelSelection;

public partial class LevelSelectionStage : Node2D
{
	private LevelSet LevelSet { get; set; }
	private Map Map { get; set; }
	
	public override void _Ready()
	{
		LevelSet = Nodes.GetRoot().DataManager.LevelSets[0];
		Map = GetNode("Map") as Map;
		
		foreach (var level in LevelSet.Levels)
		{
			var tile = Map.OverlayLayer.AddTile("LevelMarker", level.Position);
			GD.Print(level.Position);
		} 
	}
}
