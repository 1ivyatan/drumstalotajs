using Godot;
using System;
using System.Text.RegularExpressions;

namespace Drumstalotajs.Utilities;

public static class Types
{
	public static class Vector2I
	{
		private static string Vector2IFormat = @"^\(([0-9]+)\, ([0-9]+)\)$";

		public static bool ValidVector2I(string coords)
		{
			if (Regex.IsMatch(coords, Vector2IFormat))
			{
				Variant variant = GD.StrToVar($"Vector2i{coords}");
				return variant.VariantType == Variant.Type.Vector2I || 	variant.VariantType == Variant.Type.Vector2;
			}
			return false;
		}
	
		public static Godot.Vector2I StringToVector2I(string coords)
		{
			if (ValidVector2I(coords))
			{
				Variant variant = GD.StrToVar($"Vector2i{coords}");
				return variant.AsVector2I();
			} else
			{
				return Godot.Vector2I.Zero;
			}
		}
		
	}
}
