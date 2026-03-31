using Godot;
using System;

namespace Drumstalotajs.Resources.Sets.LevelSets;

[GlobalClass]
public partial class LevelSet : Resource
{
	[Export] public string Name { get; set; } = "";
}
