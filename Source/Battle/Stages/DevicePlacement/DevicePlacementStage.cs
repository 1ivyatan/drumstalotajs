using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Battle.Stages.DevicePlacement;

public partial class DevicePlacementStage : Stage
{
	private Mapping.Map map;
	private List<Vector2> placableEntitySpots;
	
	public override void _Ready()
	{
		map = GetMap();
		placableEntitySpots = new List<Vector2>();
		foreach (Vector2 position in map.MapData.PlacablePositions)
		{
			GD.Print(position);
		}
		//GD.Print(GetMap().MapData.PlacablePositions);
	}
	
	public bool CheckLimits()
	{
		return true;
	}
}
