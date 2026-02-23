using Godot;
using System;

namespace Drumstalotajs.Battle
{
	public static class Physics
	{
		public const double Gravity = 9.81;
		public const double ScaleHeight = 8500.0;
		public const double SeaLevelAirDensity = 1.225;
		public const int Pixels = 80;
	
		public static double ToRadians(double degrees)
		{
			return (Math.PI / 180) * degrees;
		}
	}
}
