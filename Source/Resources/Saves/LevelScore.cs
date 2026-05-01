using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Resources.Levels;

namespace Drumstalotajs.Resources.Saves;

[GlobalClass]
public partial class LevelScore : Resource
{
	[Export] public int Order { get; set; }
	public LevelScore() {}
	public LevelScore(LevelProps levelProps)
	{
		Order = levelProps.Order;
	}
}
