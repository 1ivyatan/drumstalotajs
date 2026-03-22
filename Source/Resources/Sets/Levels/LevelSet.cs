using Godot;
using System;

namespace drumstalotajs.Resources.Sets.Levels;

[GlobalClass]
public partial class LevelSet : Resource
{
	[Export] public string Name { get; set; }
	[Export] public LevelProperties[] Levels { get; set; }
	[Export] public Maps.Meta BackgroundMap { get; set; }
}
