using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Resources.Saves;

[GlobalClass]
public partial class Save : Resource
{
	[Export] public Dictionary<int, Array<LevelScore>> Scores = new();
}
