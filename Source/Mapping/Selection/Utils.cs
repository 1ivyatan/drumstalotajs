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
}
