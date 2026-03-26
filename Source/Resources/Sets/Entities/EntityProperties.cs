using Godot;
using System;
using Godot.Collections;

namespace drumstalotajs.Resources.Sets.Entities;

[GlobalClass]
public partial class EntityProperties : Resource
{
	[Export] public int Id { get; set; }
	[Export] public Texture2D Icon { get; set; }
	[Export] public PackedScene Scene { get; set; }
}
