using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Resources.Saves;

[GlobalClass]
public partial class LevelScore : Resource
{
	[Export] public int Order { get; set; }
}
