using Godot;
using System;

public static class Physics
{
	/* constants */
	public const double Gravity = 9.81f;
	public const int Pixels = 80;
	
	/* helper methods */
	public static double ToRadians(double degrees)
	{
		return (Math.PI / 180) * degrees;
	}
}
