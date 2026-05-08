using System;
using Godot;

namespace Drumstalotajs.Utilities;

public static class Calculations
{
	/*
		Thanks for this:
		https://stackoverflow.com/questions/13975745/the-fastest-way-to-get-current-quadrant-of-an-angle
	*/
	
	public static int GetQuadrant(double degrees)
	{
		degrees %= 360.0;
		if (degrees < 0) degrees += 360.0;
		return ((int)degrees/90) % 4 + 1;
	}
	
	public static double ToRadians(double degrees)
	{
		return (Math.PI / 180) * degrees;
	}
		
	public static Vector2 AzimuthToDirection(double azimuth)
	{
		double radians = ToRadians(90.0 - azimuth);
		return new Vector2((float)Math.Cos(radians), (float)-Math.Sign(Math.Sin(radians)));
	}
		
	public static double GetAirDensity(double altitude)
	{
		return Constants.Physics.SeaLevelAirDensity * Math.Exp(-altitude / Constants.Physics.ScaleHeight);
	}
}
