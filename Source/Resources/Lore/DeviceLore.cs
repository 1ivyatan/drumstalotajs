using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Resources;
using Drumstalotajs.Resources.Mapping;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Utilities;
using Drumstalotajs.Managers.Audio;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Resources.Levels;

[GlobalClass]
public partial class DeviceLore : Resource
{
	[Export] public Texture2D Image { get; set; } = null;
	[Export] public DevicePropertiesData DeviceProps { get; set; } = null;
}
