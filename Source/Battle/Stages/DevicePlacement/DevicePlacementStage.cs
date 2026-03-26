using Godot;
using System;
using System.Collections.Generic;
using drumstalotajs.Mapping.Selection.Filtering;

namespace drumstalotajs.Battle.Stages.DevicePlacement;

public partial class DevicePlacementStage : Stage
{
	private Mapping.Map map;
	private List<Entities.Entity> placableEntitySpots;
	
	public override void _Ready()
	{
		map = GetMap();
		map.Selector.Filter.Layer = SelectionLayer.ALL;
		map.Selector.Filter.EntityIds = [2, 3];
		//map.Selector.LayerFilter = Selector.SelectionLayer.ENTITY;
		placableEntitySpots = new List<Entities.Entity>();
		foreach (Vector2 position in map.MapData.PlacablePositions)
		{
			Entities.Entity marker = map.EntityLayer.SpawnEntity(position, 3);
			placableEntitySpots.Add(marker);
		}
		
		map.Selector.ClickedEntity += ClickedEntity;
		//GD.Print(GetMap().MapData.PlacablePositions);
	}
	
	private void ClickedEntity(Entities.Entity marker)
	{
		GD.Print(marker );
	}
	
	public bool CheckLimits()
	{
		return true;
	}
}
