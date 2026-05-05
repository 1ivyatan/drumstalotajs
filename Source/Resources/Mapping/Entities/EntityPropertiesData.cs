using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Resources.Mapping.Entities;

[GlobalClass]
public partial class EntityPropertiesData : Resource
{
	[Export] public double Height { get; set; }
}
