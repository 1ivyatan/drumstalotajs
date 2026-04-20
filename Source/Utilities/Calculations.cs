using System;

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
}
