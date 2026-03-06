using Godot;
using System;

namespace Drumstalotajs.Resources.Levels
{
	[GlobalClass]
	public partial class Pattern : Resource
	{
		[Export] public Vector2I Offset { get; set; }
		[Export] public TileMapPattern Tiles { get; set; }
	}
}
