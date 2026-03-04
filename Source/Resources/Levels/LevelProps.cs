using Godot;
using System;

namespace Drumstalotajs.Resources.Levels
{
	[GlobalClass]
	public partial class LevelProps : Resource
	{
		[Export] public bool Unlocked { get; set; }
		[Export] public Vector2 Position { get; set; }
		[Export] public Levels.Level Level { get; set; }
	}
}
