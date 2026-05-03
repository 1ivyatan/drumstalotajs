using Godot;
using System;
using System.Linq;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Battle;
using Drumstalotajs.Mapping;
using Drumstalotajs.Resources.Mapping.Sets;
using Drumstalotajs.Mapping.Entities;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Mapping.Overlays;

namespace Drumstalotajs.Battle.Stages;

public partial class DevicePlacement : Control
{
	[Export] private ItemList _deviceInventory;
	private BattleScene _scene;
	private Map _map;
	
	private DeviceProps _deviceProps;
	
	public async override void _Ready()
	{
		_scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
		_scene.BattleTopnav.Title = "Device placement";
		//_map.EntityLayer.InstanceCount();
		
		var deviceAtlas = _map.EntityLayer.GetFullAtlas();

		foreach (var device in _map.CurrentLoadedMap.DeviceProps)
		{
			var sceneTile = deviceAtlas.FirstOrDefault(e => e.Id == device.DeviceId);
			if (sceneTile != null && sceneTile is EntityLayerAtlasData entity && entity.Type == EntityType.Device)
			{
				_deviceInventory.AddItem($"{device.MaxCount}", entity.Thumbnail);
			}
			GD.Print(device.DeviceId);
		}
		
		foreach (var position in _map.CurrentLoadedMap.DevicePositions)
		{
			await _map.OverlayLayer.AddTile(position.Key, "DeviceMarker");
			var marker = (DeviceMarker)_map.OverlayLayer.GetInstance(position.Key);
			marker.SetArrowRotation(position.Value);
		}
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		
	}
}
