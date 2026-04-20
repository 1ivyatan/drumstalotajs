using System;
using Godot;
using Drumstalotajs;

namespace Drumstalotajs.Utilities;

public static class Constants
{
	public static class Vector2I
	{
		public static Godot.Vector2I Negative => new Godot.Vector2I(-1, -1);
	}
	
	public static class Mapping
	{
		public static Godot.Vector2I TileSize => new Godot.Vector2I(32, 32);
		public static int MaxProjectileCount => 50;
	}
}
