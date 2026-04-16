using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Cameras;

namespace Drumstalotajs.Mapping;

public partial class Map : Node2D
{
	/* Must fix this */
	public Vector2 GetLocalMousePos()
	{
		GD.Print(GetGlobalMousePosition());
		GD.Print(GetViewport().GetMousePosition());
		Vector2 mouseScreenPos = GetViewport().GetMousePosition();
		Vector2 mouseWorldPos = Camera.ScreenToWorld(mouseScreenPos);
		return GroundLayer.ToLocal(mouseWorldPos);
	}
	
	public Vector2I GetCellPosFromMouse()
	{
		return GroundLayer.LocalToMap(GetLocalMousePos());
	}
}
