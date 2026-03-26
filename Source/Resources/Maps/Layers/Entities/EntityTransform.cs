using Godot;
using System;
using Godot.Collections;

namespace drumstalotajs.Resources.Maps.Layers.Entities;

[GlobalClass]
public partial class EntityTransform : Resource
{
	[Export] public Vector2 Position { get; set; }
	[Export] public double Azimuth { get; set; }
}
