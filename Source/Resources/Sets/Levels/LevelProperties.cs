using Godot;
using System;

namespace drumstalotajs.Resources.Sets.Levels;

[GlobalClass]
public partial class LevelProperties : Resource
{
	[Export] public Maps.Meta Meta { get; set; }
	[Export] public Vector2 Position { get; set; }
}
