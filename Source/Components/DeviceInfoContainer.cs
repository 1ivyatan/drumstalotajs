using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Resources.Mapping.Sets;
using Drumstalotajs.Resources.Mapping.Entities;
using Drumstalotajs.Resources.Mapping.Layers;

namespace Drumstalotajs.Components;

public partial class DeviceInfoContainer : Control
{
	[Export] private TextureRect _icon;
	[Export] private Label _name;
	[Export] private RichTextLabel _desc;
	
	public void LoadDeviceData(EntityLayerAtlasData atlasData)
	{
		if (atlasData.Properties is DevicePropertiesData deviceProps)
		{
			_icon.Texture = atlasData.Thumbnail;
			_name.Text = deviceProps.Name;
			_desc.Text = deviceProps.Description;
			Visible = true;
		}
	}
	
	public void Close()
	{
		Visible = false;
	}
}
