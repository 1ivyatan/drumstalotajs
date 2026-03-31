using Godot;
using System;
using Drumstalotajs.Resources.Sets.LevelSets;

namespace Drumstalotajs.Resources.Saves;

[GlobalClass]
public partial class Progress : Resource
{
	[Export] public LevelSet LevelSet { get; set; } = null;
}
