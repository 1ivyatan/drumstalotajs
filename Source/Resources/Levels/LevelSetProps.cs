using Godot;
using System;

namespace Drumstalotajs.Resources.Levels;

[GlobalClass]
public partial class LevelSetProps : Resource
{
	[Export] public Vector2I Position { get; private set; }
}
