using Godot;
using System;
using System.Collections.Generic;

namespace Drumstalotajs.Battle.Stage.StageOverlays.DevicePlacement
{
	public partial class Stage : Battle.Stage.StageOverlay
	{
		private Battle.Scene _scene;
		private Battle.Map.Selector _selector;
		private Map.Layers.EntityLayer _entityLayer;
		private Button _toDeviceAdjustmentButton;
		private MapDevices _mapDevices;
		private TopPanel _topPanel;
		
		private void ClickedOnDeviceTile(int entityType, Vector2I tilePosition)
		{
			_entityLayer.InsertEntity(
				((Entities.Type)entityType == Entities.Type.DeviceMarker
					? (Entities.Id)_mapDevices.SelectedDeviceId
					: Entities.Id.DeviceMarker
				), tilePosition
			);
		}
		
		private void CheckDeviceCount(Entities.Entity entity, Vector2I tilePosition)
		{
			if ((Entities.Type)entity.EntityResource.Type == Entities.Type.Device)
			{
				if (_scene.Level.Devices.ContainsKey(entity.EntityResource.Id))
				{
					if (_entityLayer.Entitys[Entities.Type.Device].Count > 0 &&  _entityLayer.Entitys[Entities.Type.Device].Count <= _scene.Level.Devices[entity.EntityResource.Id].Amount)
					{
						_toDeviceAdjustmentButton.Disabled = false;
					} else
					{
						_toDeviceAdjustmentButton.Disabled = true;
					}
				} else
				{
					//_entityLayer.InsertEntity(Entities.Type.DeviceMarker, tilePosition);
				}
				
			}
			/*
			if ((Entities.Type)entityType == Entities.Type.Device)
			{
				if (_devices == null)
				{
					_toDeviceAdjustmentButton.Disabled = !(
						_entityLayer.EntityPointers[Entities.Type.Device].Count > 0
					);
				} else
				{
					_toDeviceAdjustmentButton.Disabled = !(
						
					);
				}
			}*/
		}
		
		public override void _Ready()
		{
			_toDeviceAdjustmentButton = GetNode<Button>("ToDeviceAdjustmentButton");
			_mapDevices = GetNode<Container>("MapDevices") as MapDevices;
			_entityLayer = GetNode<Node2D>("../../Map/EntityLayer") as Map.Layers.EntityLayer;
			_selector = GetNode<Node2D>("../../Map/Selector") as Battle.Map.Selector;
			_scene = GetNode<Control>("../../..") as Battle.Scene;
			_topPanel = GetNode<Control>("../../../TopPanel") as Battle.TopPanel;
			
			_topPanel.SetLabel("Device placement");

			_selector.Enabled = true;
			_selector.Layer = Map.Selector.SelectorLayer.Entity;
			_selector.FilterMode = Map.Selector.SelectorFilterMode.Fitlered;
			_selector.Filter = [ Battle.Entities.Type.DeviceMarker, Battle.Entities.Type.Device ];
			
			_toDeviceAdjustmentButton.Disabled = true;
			
			_selector.Connect("SelectedEntity", new Callable(this, nameof(ClickedOnDeviceTile)));
			_entityLayer.Connect("AddedEntity", new Callable(this, nameof(CheckDeviceCount)));
			_entityLayer.Connect("RemovedEntity", new Callable(this, nameof(CheckDeviceCount)));
			_toDeviceAdjustmentButton.Connect("pressed", Callable.From(() => {
				(GetParent<Control>() as Battle.Stage.Manager).DeviceAdjustment();
			}));
			
			if (_scene.Level != null)
			{
				_mapDevices.SetDevices(_scene.Level.Devices);
			}
			
			//AddedEntityEventHandler
			/*
			if (_scene.Level != null)
			{
				_devices = new Dictionary<Entities.Type, int>();
				foreach (KeyValuePair<int, int> row in _scene.Level.Devices)
				{
					_devices.Add((Entities.Type)row.Key, row.Value);
				}
			} else
			{
				_devices = null;
			}*/
			
		}
	}
}
