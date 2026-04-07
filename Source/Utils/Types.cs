using Godot;
using System;

namespace Drumstalotajs.Utils;

public static class Types
{
	public static bool ValidVector2I(string coords)
	{
		Variant variant = GD.StrToVar(coords);
		return variant.VariantType == Variant.Type.Vector2I || variant.VariantType == Variant.Type.Vector2;
	}
	
	public static  Vector2I StringToVector2I(string coords)
	{
		Variant variant = GD.StrToVar(coords);
		return variant.AsVector2I();
	}
}
