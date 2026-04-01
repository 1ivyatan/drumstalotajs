using Godot;
using System;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Resources.Levels;

[GlobalClass]
public partial class LevelSet : Resource
{
	[Export] public string Name { get; private set; }
	[Export] public MapMeta BackgroundMapMeta { get; private set; }
	[Export] public LevelSetProps[] Levels { get; private set; }
}
