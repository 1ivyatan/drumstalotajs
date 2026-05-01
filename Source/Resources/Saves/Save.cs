using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Resources.Levels;

namespace Drumstalotajs.Resources.Saves;

[GlobalClass]
public partial class Save : Resource
{
	[Export] public Dictionary<LevelSet, Array<LevelScore>> Scores = new();
}
