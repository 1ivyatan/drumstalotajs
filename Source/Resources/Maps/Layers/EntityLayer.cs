using Godot;
using System;
using Godot.Collections;

namespace drumstalotajs.Resources.Maps.Layers;

[GlobalClass]
public partial class EntityLayer : Resource
{
	[Export] public Dictionary<int, Godot.Collections.Array<Entities.EntityProperties>> Entities { get; set; }
}
