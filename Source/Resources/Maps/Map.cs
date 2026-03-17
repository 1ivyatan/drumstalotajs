using Godot;
using System;
using Godot.Collections;

namespace drumstalotajs.Resources.Maps;

[GlobalClass]
public partial class Map : Resource
{
	[Export] public Layers.GroundLayer GroundLayer { get; set; }
	[Export] public Layers.DecorationLayer DecorationLayer { get; set; }
	[Export] public Layers.EntityLayer EntityLayer { get; set; }
}
