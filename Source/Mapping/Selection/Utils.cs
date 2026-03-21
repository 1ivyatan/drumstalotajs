using Godot;
using System;

namespace drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	private Vector2 GetLocalPos()
	{
		Vector2 globalMousePos = GetGlobalMousePosition();
		return map.GroundLayer.ToLocal(globalMousePos);
	}
	
	private Vector2I GetCellPos(Vector2 localMousePos)
	{
		return map.GroundLayer.LocalToMap(localMousePos);
	}
	
	private Vector2 GetLocalMousePos()
	{
		Vector2 mouseScreenPos = GetViewport().GetMousePosition();
		Vector2 mouseWorldPos = camera.ScreenToWorld(mouseScreenPos);
		return map.GroundLayer.ToLocal(mouseWorldPos);
	}
	
	private Vector2I GetCellPosFromMouse()
	{
		return map.GroundLayer.LocalToMap(GetLocalPos());
	}
}
