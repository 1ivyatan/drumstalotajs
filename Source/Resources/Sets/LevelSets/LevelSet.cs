using Godot;
using System;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Resources.Sets.LevelSets;

[GlobalClass]
public partial class LevelSet : Resource
{
	[Export] public string Name { get; set; } = "";
	[Export] public MapMeta BackgroundMapMeta { get; set; }
}
