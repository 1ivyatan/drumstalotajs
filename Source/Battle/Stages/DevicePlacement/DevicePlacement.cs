using Godot;
using System;

namespace Drumstalotajs.Battle.Stages
{
	public partial class DevicePlacement : StageOverlay
	{
		private Battle.Map.Selector _selector;
		private Battle.Map.EntityLayer _entityLayer;
		private Button _toDeviceAdjustmentButton;
		
		private void ClickedOnDeviceTile(int entityType, Vector2I tilePosition)
		{
			_entityLayer.InsertEntity((
				(Entities.Type)entityType == Entities.Type.DeviceMarker
					? Entities.Type.Device
					: Entities.Type.DeviceMarker
			), tilePosition);
		}
		
		private void CheckDeviceCount(int entityType)
		{
			if ((Entities.Type)entityType == Entities.Type.Device)
			{
				_toDeviceAdjustmentButton.Disabled = !(_entityLayer.EntityPointers[Entities.Type.Device].Count > 0);
			}
		}
		
		public override void _Ready()
		{
			_toDeviceAdjustmentButton = GetNode<Button>("ToDeviceAdjustmentButton");
			_entityLayer = GetNode<Node2D>("../../Map/EntityLayer") as Battle.Map.EntityLayer;
			_selector = GetNode<Node2D>("../../Map/Selector") as Battle.Map.Selector;

			_selector.Enabled = true;
			_selector.Layer = Map.Selector.SelectorLayer.Entity;
			_selector.FilterMode = Map.Selector.SelectorFilterMode.Fitlered;
			_selector.Filter = [ Battle.Entities.Type.DeviceMarker, Battle.Entities.Type.Device ];
			_toDeviceAdjustmentButton.Disabled = true;
			
			_selector.Connect("SelectedEntity", new Callable(this, nameof(ClickedOnDeviceTile)));
			_entityLayer.Connect("ChangeInEntities", new Callable(this, nameof(CheckDeviceCount)));
			_toDeviceAdjustmentButton.Connect("pressed", Callable.From(() => {
				(GetParent<Control>() as Stages.Manager).DeviceAdjustment();
			}));
		}
	}
}
