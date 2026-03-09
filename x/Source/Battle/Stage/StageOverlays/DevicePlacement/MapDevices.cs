using Godot;
using Godot.Collections;
using System;
using System.Linq;

namespace Drumstalotajs.Battle.Stage.StageOverlays.DevicePlacement
{
	public partial class MapDevices : Container
	{
		public int SelectedDeviceId { get; private set; }
		
		public void SetDevices(Dictionary<int, Resources.Levels.DeviceProps> deviceData)
		{
			PackedScene deviceSelector = ResourceLoader.Load<PackedScene>("res://Scenes/Battle/StageOverlays/DevicePlacement/DeviceButton.tscn");
			
			foreach (var key in deviceData.Keys)
			{
				DeviceButton button = deviceSelector.Instantiate<DeviceButton>();
				button.PrepareButton(key);
				AddChild(button);
			}
			
			SelectedDeviceId = deviceData.Keys.FirstOrDefault();
		}
	}
}
