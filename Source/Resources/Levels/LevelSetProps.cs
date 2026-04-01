using Godot;
using System;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Resources.Levels;

[GlobalClass]
public partial class LevelSetProps : Resource
{
	[Export] public Vector2I Position { get; private set; }
	[Export] public int Order { get; private set; }
	[Export] public MapMeta Meta { get; private set; }
}
