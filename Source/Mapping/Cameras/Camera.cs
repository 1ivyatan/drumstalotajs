using Godot;
using System;

namespace Drumstalotajs.Mapping.Cameras;

public partial class Camera : Camera2D
{
	public CameraMode Mode { get; set; } = CameraMode.LOCK;
	public CameraState State { get; private set { field = value; } } = CameraState.IDLE;
	
}
