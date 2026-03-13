using Godot;
using System;

namespace drumstalotajs.Mapping;

public partial class MapCamera : Camera2D
{
	public enum DraggingState { NONE, HORIZONTAL, VERTICAL, ALL }
}
