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
using Drumstalotajs.Battle.Components;
using Drumstalotajs.Battle.Stages;

namespace Drumstalotajs.Battle.Stages;

public partial class DeviceAdjustment : Control
{
	[Export] private DeviceAdjustmentContainer _deviceAdjustmentContainer;
	[Export] private Button _toFiringButton;
	private BattleScene _scene;
	private Map _map;

	public override void _Ready()
	{
		_scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
		_scene.BattleTopnav.Title = "Adjust devices";
		
		BaseLayer[] layers = [ _map.EntityLayer ];
		FilteredItemIds idFilters = new FilteredItemIds
		{
			{ _map.EntityLayer, _map.EntityLayer.GetEntityIdsByType(EntityType.Device) }
		};
		
		_toFiringButton.Pressed += () => {
			_map.OverlayLayer.ClearAllHighlighters();
			if (_scene.ScoreManager.CanContinue())
			{
				_scene.StageManager.Firing(FiringMode.Both);
			} else
			{
				_scene.StageManager.End();
			}
		};
		
		_map.Selector.Filter = new SelectorFilter(layers, idFilters);
		_map.Mode = MapMode.Interactable;
		
		_map.OverlayLayer.RemoveAllInstancesByName("DeviceMarker");
		_map.OverlayLayer.ClearAllHighlighters();
		_scene.ScoreManager.NextTurn();
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
				var pos = _map.ViewportMouseToMap();
				var tiles = _map.Flash(pos);
				
				if (tiles.Count > 0)
				{
					var tile = tiles[_map.EntityLayer][0];
					if ((SceneTile)tile is Device device && device.Player)
					{
						_map.OverlayLayer.ClearAllHighlighters();
						_map.OverlayLayer.PlaceHighlighter(_map.OverlayLayer.LocalToMap(device.Position));
						_deviceAdjustmentContainer.Load(device);
					}
				} else
				{
					_map.OverlayLayer.ClearAllHighlighters();
					_deviceAdjustmentContainer.Close();
				}
			}
		}
	}
}
