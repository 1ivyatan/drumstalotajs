using Godot;
using System;

namespace Drumstalotajs.Resources.Levels
{
	[GlobalClass]
	public partial class DeviceProps : Resource
	{
		[Export] public int Amount { get; set; }
		[Export] public int[] AllowedIndices { get; set; }
	}
}
