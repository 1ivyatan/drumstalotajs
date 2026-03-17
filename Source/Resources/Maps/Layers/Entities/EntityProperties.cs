using Godot;
using System;
using Godot.Collections;

namespace drumstalotajs.Resources.Maps.Layers.Entities;

[GlobalClass]
public partial class EntityProperties : Resource
{
	[Export] public Vector2 Position { get; set; }
	[Export] public double Azimuth { get; set; }
	
	//[Export] public Dictionary<int, Godot.Collections.Array<Vector2>> Entities { get; set; }
}
