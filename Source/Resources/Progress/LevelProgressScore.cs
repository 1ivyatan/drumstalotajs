using Godot;
using System;
using Drumstalotajs.Resources.Levels;

namespace Drumstalotajs.Resources.Progress;

[GlobalClass]
public partial class LevelProgressScore : Resource
{
	public LevelSetProps LevelProps { get; private set; }
	
}
