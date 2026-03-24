using Godot;
using System;
using Godot.Collections;

namespace drumstalotajs.Resources.Maps.Layers.Entities;

[GlobalClass]
public partial class PlacableEntityProperties : Resource
{
	[Export] public int Id { get; set; }
	[Export] public int Min { get; set; }
	[Export] public int Max { get; set; }
}
