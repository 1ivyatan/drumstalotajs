using Godot;
using System;

namespace Drumstalotajs.Mapping.Selection;

//public partial class Utils : Node

public partial class Selector : Node2D
{
	public Vector2 GetMousePosition()
	{
		/* vvvvvvvvvvvvvv */ //GroundLayer
		return GetLocalMousePosition();
	}
	
	public Vector2I GetMousePositionTile()
	{
		/* vvvvvvvvvvvvvv */ //GroundLayer
		Vector2 localPos = GetMousePosition();
		return _map.GroundLayer.LocalToMap(localPos);
	}
}
