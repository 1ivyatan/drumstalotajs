using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Resources.Mapping.Entities;

[GlobalClass]
public partial class WallPropertiesData : EntityPropertiesData
{
	[Export] public double Height { get; private set; }
}
