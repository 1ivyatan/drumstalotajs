using Godot;
using System;
using System.Collections.Generic;

namespace Drumstalotajs.Battle.Stages
{
	public partial class DevicePlacement : StageOverlay
	{
		private Battle.Scene _scene;
		private Battle.Map.Selector _selector;
		private Battle.Map.EntityLayer _entityLayer;
		private Button _toDeviceAdjustmentButton;
		
		private void ClickedOnDeviceTile(int entityType, Vector2I tilePosition)
		{
			//if (_scene.Level.Devices.ContainsKey)
			//{
		//		return;
		//	}
			
			_entityLayer.InsertEntity((
				(Entities.Type)entityType == Entities.Type.DeviceMarker
					? Entities.Type.Device
					: Entities.Type.DeviceMarker
			), tilePosition);
		}
		
		private void CheckDeviceCount(Entities.Entity entity, Vector2I tilePosition)
		{
			GD.Print(entity.EntityResource.Type);
			if ((Entities.Type)entity.EntityResource.Type == Entities.Type.Device)
			{
				if (_scene.Level.Devices.ContainsKey(entity.EntityResource.Id))
				{
					if (_entityLayer.EntityPointers[Entities.Type.Device].Count > 0 &&  _entityLayer.EntityPointers[Entities.Type.Device].Count <= _scene.Level.Devices[entity.EntityResource.Id])
					{
						_toDeviceAdjustmentButton.Disabled = false;
					} else
					{
						_toDeviceAdjustmentButton.Disabled = true;
					}
					GD.Print(_scene.Level.Devices[entity.EntityResource.Id]);
				} else
				{
					_entityLayer.InsertEntity(Entities.Type.DeviceMarker, tilePosition);
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
			_entityLayer = GetNode<Node2D>("../../Map/EntityLayer") as Battle.Map.EntityLayer;
			_selector = GetNode<Node2D>("../../Map/Selector") as Battle.Map.Selector;
			_scene = GetNode<Control>("../../..") as Battle.Scene;

			_selector.Enabled = true;
			_selector.Layer = Map.Selector.SelectorLayer.Entity;
			_selector.FilterMode = Map.Selector.SelectorFilterMode.Fitlered;
			_selector.Filter = [ Battle.Entities.Type.DeviceMarker, Battle.Entities.Type.Device ];
			
			_toDeviceAdjustmentButton.Disabled = true;
			
			_selector.Connect("SelectedEntity", new Callable(this, nameof(ClickedOnDeviceTile)));
			_entityLayer.Connect("AddedEntity", new Callable(this, nameof(CheckDeviceCount)));
			_entityLayer.Connect("RemovedEntity", new Callable(this, nameof(CheckDeviceCount)));
			_toDeviceAdjustmentButton.Connect("pressed", Callable.From(() => {
				(GetParent<Control>() as Stages.Manager).DeviceAdjustment();
			}));
			
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
