using Godot;
using System;

namespace drumstalotajs.Utilities;

public static class Topography
{
	public static readonly int TileSize = 16;
	public static readonly Vector2I NegativeVector = new Vector2I(-1, -1);
	public static Vector2 AzimuthToDirection(double azimuth)
	{
		double radians = Physics.ToRadians(90.0 - azimuth);
		return new Vector2((float)Math.Cos(radians), (float)-Math.Sign(Math.Sin(radians)));
	}
}
