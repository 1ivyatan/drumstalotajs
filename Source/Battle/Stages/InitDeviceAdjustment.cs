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
using Drumstalotajs.Components;

namespace Drumstalotajs.Battle.Stages;

public partial class InitDeviceAdjustment : Control
{
	[Export] private CircleSlider _azimuthSlider;
	[Export] private Label _azimuthLabel;
	[Export] private Wheel _angleSlider;
	[Export] private Label _angleLabel;
	
	private BattleScene _scene;
	private Map _map;
	
	public override void _Ready()
	{
		_scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
		_scene.BattleTopnav.Title = "Prepare devices";
		
		BaseLayer[] layers = [ _map.EntityLayer ];
		FilteredItemIds idFilters = new FilteredItemIds
		{
			{ _map.EntityLayer, _map.EntityLayer.GetEntityIdsByType(EntityType.Device) }
		};
		_map.Selector.Filter = new SelectorFilter(layers, idFilters);
		_map.Mode = MapMode.Interactable;
		
		_map.OverlayLayer.RemoveAllInstancesByName("DeviceMarker");
		_map.OverlayLayer.ClearAllHighlighters();
		
		/*
		_azimuthSlider.ValueChanged += (double value) => {
			_azimuthLabel.Text = $"~{Math.Round(value)}°";
		};
		_angleSlider.ValueChanged += (double value) => {
			_angleLabel.Text = $"~{Math.Round(value)}°";
		};*/
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
					var device = tiles[_map.EntityLayer][0];
					_map.OverlayLayer.PlaceHighlighter(_map.OverlayLayer.LocalToMap(((SceneTile)device).Position));
				} else
				{
					_map.OverlayLayer.ClearAllHighlighters();
				}
				
			}
		}
	}
				//if (_selectedDeviceAtlas == null)
				//{
				//	Nodes.GetRoot().ToastManager.SpawnOne("Select a device!");
				//	return;
				//}
}
