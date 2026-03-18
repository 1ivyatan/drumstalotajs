using Godot;
using System;

namespace drumstalotajs.Mapping.Camera;

public partial class MapCamera : Camera2D
{
	public enum MapCameraState { IDLE, ZOOM, DRAG, ZOOMDRAGGING }
	public MapCameraState State { get; private set; } = MapCameraState.IDLE;
}
