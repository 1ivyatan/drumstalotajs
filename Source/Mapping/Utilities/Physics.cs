using Godot;
using System;

namespace drumstalotajs.Utilities;

public static class Physics
{
	public static double ToRadians(double degrees)
	{
		return (Math.PI / 180) * degrees;
	}
}
