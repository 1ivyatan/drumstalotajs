using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using drumstalotajs.Mapping.Selection.Filtering;

namespace drumstalotajs.Battle.Stages.DevicePlacement;

public partial class DevicePlacementStage : Stage
{
	[Export] private Resources.Sets.Entities.EntityProperties DeviceMarker;
	
	private Mapping.Map map;
	private List<Vector2> placableEntitySpots;
	private DeviceSelectionContainer deviceSelectionContainer;
	private Button toDeviceAdjustmentStageButton;
	
	private int selectedDeviceId = -1;
	
	public override void _Ready()
	{
		deviceSelectionContainer = GetNode<Control>("DeviceSelectionContainer") as DeviceSelectionContainer;
		toDeviceAdjustmentStageButton = GetNode<Button>("ToDeviceAdjustmentStageButton");
		map = GetMap();
		map.Selector.Filter.Layer = SelectionLayer.ENTITY;
		map.Selector.Filter.EntityIds = [2, 3];
		placableEntitySpots = new List<Vector2>();
		deviceSelectionContainer.SetDevices(map, DeviceMarker, map.EntityLayer.EntitySetResource, map.MapData.PlacableEntities);
		
		deviceSelectionContainer.Selected += (int id) => { selectedDeviceId = id; };
		selectedDeviceId = deviceSelectionContainer.SelectedDeviceId;
		
		foreach (Vector2 position in map.MapData.PlacablePositions)
		{
			Entities.Entity marker = map.EntityLayer.SpawnEntity(position, 3);
			placableEntitySpots.Add(marker.Position);
		}
		
		map.Selector.ClickedEntity += ClickedEntity;
		
		toDeviceAdjustmentStageButton.Pressed += () => {
			if (CheckLimits())
			{
				GD.Print("pass");
			}
		};
	}
	
	private void ClickedEntity(Entities.Entity entity)
	{
		if (placableEntitySpots.Contains(entity.Position) && selectedDeviceId != -1)
		{
			int oldSelDevicecount = map.EntityLayer.GetEntitiesById(selectedDeviceId).Length;
			int oldId = entity.EntityResource.Id;
			int oldCount = map.EntityLayer.GetEntitiesByType(Entities.EntityType.DEVICE).Length;
			int newSelDeviceCount = oldSelDevicecount + (oldId == DeviceMarker.Id ? 1 : -1);
			int newCount = oldCount + (oldId == DeviceMarker.Id ? 1 : -1);
			var mapData = map.MapData;
			var props = map.MapData.GetPlacableEntityProperties(selectedDeviceId);
			
			if (newSelDeviceCount > props.Max || newCount > mapData.MaxPlacableEntities) return;
			
			Vector2 position = entity.Position;
			map.EntityLayer.RemoveEntity(entity);
			map.EntityLayer.SpawnEntity(position, (oldId == DeviceMarker.Id) ? selectedDeviceId : DeviceMarker.Id);
			toDeviceAdjustmentStageButton.Disabled = !CheckLimits();
		}
	}
	
	public bool CheckLimits()
	{
		var mapData = map.MapData;
		
		return true;
	}
}
