using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Resources.Mapping.Entities;

[GlobalClass]
public partial class EntityPropertiesData : Resource
{
	[Export] public double Height { get; set; }
	[Export(PropertyHint.Range, "-10,10,0.01")] public double Toughness { get; set; }
}
