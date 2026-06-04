using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Resources.Mapping.Sets;
using Drumstalotajs.Resources.Mapping.Entities;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Battle.Components;

public partial class FireTracker : ItemList
{
	
	public override void _Ready()
	{
	}
	
	public void AddDevice(Device device, EntityLayerAtlasData atlasData)
	{
		if (atlasData.Properties is DevicePropertiesData deviceProps)
		{
			AddItem($":D", atlasData.Thumbnail);
			//_icon.Texture = atlasData.Thumbnail;
			//_name.Text = deviceProps.Name;
			//_desc.Text = deviceProps.Description;
		}
	}
	
	public void SubtractDeviceShell(Device device)
	{
		
	}
	
	public void Clear()
	{
		
	}
}
