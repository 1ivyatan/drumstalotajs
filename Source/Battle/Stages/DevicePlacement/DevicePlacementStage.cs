using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using drumstalotajs.Mapping.Selection.Filtering;

namespace drumstalotajs.Battle.Stages.DevicePlacement;

public partial class DevicePlacementStage : Stage
{
	private Mapping.Map map;
	private List<Vector2> placableEntitySpots;
	private DeviceSelectionContainer deviceSelectionContainer;
	
	public override void _Ready()
	{
		deviceSelectionContainer = GetNode<Control>("DeviceSelectionContainer") as DeviceSelectionContainer;
		map = GetMap();
		map.Selector.Filter.Layer = SelectionLayer.ENTITY;
		map.Selector.Filter.EntityIds = [2, 3];
		placableEntitySpots = new List<Vector2>();
		deviceSelectionContainer.SetDevices(map.EntityLayer.EntitySetResource, map.MapData.PlacableEntities);
		
		
		/*
	[Export] public Layers.Entities.PlacableEntityProperties[] PlacableEntities { get; set; }*/
		
		foreach (Vector2 position in map.MapData.PlacablePositions)
		{
			Entities.Entity marker = map.EntityLayer.SpawnEntity(position, 3);
			placableEntitySpots.Add(marker.Position);
		}
		
		map.Selector.ClickedEntity += ClickedEntity;
		//GD.Print(GetMap().MapData.PlacablePositions);
	}
	
	private void ClickedEntity(Entities.Entity entity)
	{
		if (placableEntitySpots.Contains(entity.Position))
		{
			Vector2 pos = entity.Position;
			
		}
		//if ()
	}
	
	public bool CheckLimits()
	{
		return true;
	}
}
