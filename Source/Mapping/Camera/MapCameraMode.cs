using Godot;
using System;

namespace drumstalotajs.Mapping.Camera;

public partial class MapCamera : Camera2D
{
	public enum MapCameraMode { LOCK, VIEW }
	public MapCameraMode Mode { get; set; } 
}
