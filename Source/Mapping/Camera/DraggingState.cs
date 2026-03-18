using Godot;
using System;

namespace drumstalotajs.Mapping.Camera;

public partial class MapCamera : Camera2D
{
	public enum DraggingState { NONE, HORIZONTAL, VERTICAL, ALL }
	
	public DraggingState Drag { get; private set; }
}
