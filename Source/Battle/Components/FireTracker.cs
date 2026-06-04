using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Resources.Mapping.Sets;
using Drumstalotajs.Resources.Mapping.Entities;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Battle.Components;

public partial class FireTracker : ItemList
{
	private int _head = 0;
	private Dictionary<Device, int> _devices = new();
	
	public void AddDevice(Device device, EntityLayerAtlasData atlasData, string milCoords)
	{
		if (atlasData.Properties is DevicePropertiesData deviceProps)
		{
			var countText = device.Shells > 0 ? $"{device.ShellsPerTurn}" : "Reloading";
			AddItem($"{milCoords}\n{countText}", atlasData.Thumbnail);
			_devices.Add(device, _head);
			_head++;
		}
	}
	
	public void UpdateDeviceData(Device device, int count)
	{
		if (_devices.ContainsKey(device))
		{
			int id = _devices[device];
			var oldText = GetItemText(id);
			var pos = oldText.Split(new[] {"\n"}, 2, StringSplitOptions.None);
			var remaining = device.ShellsPerTurn - (count + 1);
			var countText = device.Shells > 0 ? (
				remaining > 0
					? $"{remaining}"
					: "Done!"
			) : "Reloading";
			SetItemText(id, $"{pos[0]}\n{countText}");
		}
	}
	
	new public void Clear()
	{
		_head = 0;
		base.Clear();
	}
}
