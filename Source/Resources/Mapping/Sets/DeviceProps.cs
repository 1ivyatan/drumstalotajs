using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Resources.Mapping.Sets;

[GlobalClass]
public partial class DeviceProps : Resource
{
	[Export] public int DeviceId { get; set; } = -1;
	[Export] public int MaxCount { get; set; } = 1;
}
