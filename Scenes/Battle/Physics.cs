using Godot;
using System;

public static class Physics
{
	public const float Gravity = 9.81f;
	public static float ToRadians(float degrees)
	{
		return ((float)Math.PI / 180) * degrees;
	}
}
