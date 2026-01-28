using Godot;
using System;

namespace Drumstalotajs.Battle
{
	public static class Physics
	{
		public const double Gravity = 9.81f;
		public const int Pixels = 80;
	
		public static double ToRadians(double degrees)
		{
			return (Math.PI / 180) * degrees;
		}
	}
}
