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
	[Export] private Label _deviceCountLabel;
	private BattleScene _scene;
	private Map _map;
	
	private DeviceProps _deviceProps;
	private EntityLayerAtlasData[] _deviceAtlas;
	private SceneLayerAtlasData _selectedDeviceAtlas = null;
	private long _itemListIndex = -1;
	/* id, count */
	private Dictionary<int, int> _counter = new();
	private Array<int> _deviceIds = new();
	
	private bool _loaded = false;
	private int _loadedCount = 0;
	
	public async override void _Ready()
	{
		_scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
		_deviceAtlas = _map.EntityLayer.GetFullAtlas()
		.Where(a => a is EntityLayerAtlasData)
		.Select(a => a as EntityLayerAtlasData)
		.Where(a => a.Type == EntityType.Device)
		.ToArray();
		_scene.BattleTopnav.Title = "Device placement";
		_deviceCountLabel.Text = $"Placed: 0; Limits: {_map.CurrentLoadedMap.MinTotalDevices}-{_map.CurrentLoadedMap.MaxTotalDevices}";
		
		foreach (var device in _map.CurrentLoadedMap.DeviceProps)
		{
			var sceneTile = _deviceAtlas.FirstOrDefault(e => e.Id == device.DeviceId);
			if (sceneTile != null && sceneTile.Type == EntityType.Device)
			{
				_deviceInventory.AddItem($"{device.MaxCount}", sceneTile.Thumbnail);
				_counter[sceneTile.Id] = 0;
				_deviceIds.Add(sceneTile.Id);
			}
		}
		
		BaseLayer[] layers = [ _map.EntityLayer, _map.OverlayLayer ];
		FilteredItemIds idFilters = new FilteredItemIds
		{
			{ _map.EntityLayer, _map.EntityLayer.GetEntityIdsByType(EntityType.Device) },
			{ _map.OverlayLayer, [ _map.OverlayLayer.GetAtlasId("DeviceMarker") ] }
		};
		
		foreach (var position in _map.CurrentLoadedMap.DevicePositions)
		{
			_map.OverlayLayer.AddTile(position.Key, "DeviceMarker");
			//await ToSignal(_map.OverlayLayer, "TileSpawned");
		}
		
		_map.OverlayLayer.TileSpawned += (SceneTile tile) => {
			if (tile is DeviceMarker deviceMarker)
			{
				var pos = _map.OverlayLayer.LocalToMap(deviceMarker.Position);
				var devPosRot = _map.CurrentLoadedMap.DevicePositions[pos];
				deviceMarker.SetArrowRotation(devPosRot);
				_loadedCount++;
				if (_loadedCount == _map.CurrentLoadedMap.DevicePositions.Count)
				{
					_loaded = true;
				}
			}
		};
		
		_map.Selector.Filter = new SelectorFilter(layers, idFilters);
		_map.Mode = MapMode.HiddenInteractable;
		_deviceInventory.ItemSelected += SetSelectedDevice;
		_toDeviceAdjustment.Pressed += () => {
			if (CheckBounds())
			{
				_map.OverlayLayer.RemoveAllInstancesByName("DeviceMarker");
				_map.OverlayLayer.ClearAllHighlighters();
				_scene.StageManager.InitDeviceAdjustment();
			}
		};
	}
	
	private void SetSelectedDevice(long index)
	{
		int id = _deviceIds[(int)index];
		var atlas = _deviceAtlas.FirstOrDefault(d => d.Id == id);
		var props = _map.CurrentLoadedMap.DeviceProps.FirstOrDefault(d => d.DeviceId == id);
		_selectedDeviceAtlas = atlas;
		_deviceProps = props;
		_itemListIndex = index;
	}
	
	public async override void _UnhandledInput(InputEvent @event)
	{
		if (!_loaded) return;

		if (@event is InputEventMouse mouseEvent)
		{
			bool moving = false;
			
			if (mouseEvent is InputEventMouseMotion mouseMotion)
			{
				moving = true;
			}
			
			if (mouseEvent is InputEventMouseButton mouseButton && 
				mouseButton.ButtonIndex == MouseButton.Left &&
				mouseButton.Pressed && !moving
			)
			{	
				var tiles = _map.Flash(_map.ViewportMouseToMap());
				
				if (tiles.Count > 0)
				{
					if (_selectedDeviceAtlas == null)
					{
						Nodes.GetRoot().ToastManager.SpawnOne("Select a device!");
						return;
					}
				
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
				_map.EntityLayer.AddTile(data);
				await ToSignal(_map.EntityLayer, "TileSpawned");
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
		_deviceCountLabel.Text = $"Placed: {total}; Limits: {_map.CurrentLoadedMap.MinTotalDevices}-{_map.CurrentLoadedMap.MaxTotalDevices}";
		return (total >= _map.CurrentLoadedMap.MinTotalDevices && total <= _map.CurrentLoadedMap.MaxTotalDevices);
	}
}
