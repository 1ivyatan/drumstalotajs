using Godot;
using Godot.Collections;
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
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;
using System.Threading.Tasks;

namespace Drumstalotajs.Battle.Stages;

public partial class DevicePlacement : Control
{
	[Export] private ItemList _deviceInventory;
	[Export] private Button _toDeviceAdjustment;
	private BattleScene _scene;
	private Map _map;
	
	private DeviceProps _deviceProps;
	private SceneLayerAtlasData[] _deviceAtlas;
	private SceneLayerAtlasData _selectedDeviceAtlas = null;
	private long _itemListIndex = -1;
	/* id, count */
	private Dictionary<int, int> _counter = new();
	
	public async override void _Ready()
	{
		_scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
		_deviceAtlas = _map.EntityLayer.GetFullAtlas()
		.Where(s => s is EntityLayerAtlasData entity && entity.Type == EntityType.Device)
		.ToArray();
		_scene.BattleTopnav.Title = "Device placement";
		
		BaseLayer[] layers = [ _map.EntityLayer, _map.OverlayLayer ];
		FilteredItemIds idFilters = new FilteredItemIds
		{
			{ _map.EntityLayer, _map.EntityLayer.GetEntityIdsByType(EntityType.Device) },
			{ _map.OverlayLayer, [ _map.OverlayLayer.GetAtlasId("DeviceMarker") ] }
		};
		_map.Selector.Filter = new SelectorFilter(layers, idFilters);
		_map.Mode = MapMode.HiddenInteractable;

		foreach (var device in _map.CurrentLoadedMap.DeviceProps)
		{
			var sceneTile = _deviceAtlas.FirstOrDefault(e => e.Id == device.DeviceId);
			if (sceneTile != null && sceneTile is EntityLayerAtlasData entity && entity.Type == EntityType.Device)
			{
				_deviceInventory.AddItem($"{device.MaxCount}", entity.Thumbnail);
				_counter[device.DeviceId] = 0;
			}
		}
		
		foreach (var position in _map.CurrentLoadedMap.DevicePositions)
		{
			await _map.OverlayLayer.AddTile(position.Key, "DeviceMarker");
			var marker = (DeviceMarker)_map.OverlayLayer.GetInstance(position.Key);
			marker.SetArrowRotation(position.Value);
		}
		
		_deviceInventory.ItemSelected += this.SetSelectedDevice;
		_toDeviceAdjustment.Pressed += () => {
			if (CheckBounds())
			{
				
			}
		};
	}
	
	private void SetSelectedDevice(long index)
	{
		var atlas = _deviceAtlas[index];
		_selectedDeviceAtlas = atlas;
		_deviceProps = _map.CurrentLoadedMap.DeviceProps.FirstOrDefault(d => d.DeviceId == _selectedDeviceAtlas.Id);
		_itemListIndex = index;
	}
	
	public async override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouse mouseEvent)
		{
			if (mouseEvent is InputEventMouseButton mouseButton && 
				mouseButton.ButtonIndex == MouseButton.Left &&
				mouseButton.Pressed
			)
			{
				if (_selectedDeviceAtlas == null)
				{
					Nodes.GetRoot().ToastManager.SpawnOne("Select a device!");
					return;
				}
				
				var tiles = _map.Flash(_map.ViewportMouseToMap());
				
				if (tiles.Count > 0)
				{
					if (tiles.ContainsKey(_map.OverlayLayer) && tiles[_map.OverlayLayer].Count > 0)
					{
						var pos = _map.OverlayLayer.LocalToMap(
							((SceneTile)tiles[_map.OverlayLayer][0]).Position
						);
						await ToggleDevice(pos);
					}
				}
			}
		}
	}
	
	private async Task ToggleDevice(Vector2I position)
	{
		var device = _map.EntityLayer.GetInstance(position);
		var count = _map.EntityLayer.InstanceCount(_selectedDeviceAtlas.Id, true);
		if (device == null)
		{
			if (count < _deviceProps.MaxCount)
			{
				var data = new EntityLayerTileData();
				data.Id = _selectedDeviceAtlas.Id;
				data.Position = position;
				data.Player = true;
				await _map.EntityLayer.AddTile(data);
				_deviceInventory.SetItemText((int)_itemListIndex, $"{_deviceProps.MaxCount - count - 1}");
				_counter[_selectedDeviceAtlas.Id]++;
			}
		} else if (_selectedDeviceAtlas.Id == device.TileId)
		{
			_map.EntityLayer.RemoveTile(position);
			_deviceInventory.SetItemText((int)_itemListIndex, $"{_deviceProps.MaxCount - count + 1}");
			_counter[_selectedDeviceAtlas.Id]--;
		}
		
		LockcheckAdjustmentButton();
	}
	
	private void LockcheckAdjustmentButton()
	{
		_toDeviceAdjustment.Disabled = !CheckBounds();
	}
	
	private bool CheckBounds()
	{
		int total = 0;
		foreach (var count in _counter)
		{
			total += count.Value;
		}
		return (total >= _map.CurrentLoadedMap.MinTotalDevices && total <= _map.CurrentLoadedMap.MaxTotalDevices);
	}
}
