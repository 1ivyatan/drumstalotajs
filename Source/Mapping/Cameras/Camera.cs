using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Cameras;

public partial class Camera : Camera2D
{
	public CameraMode Mode { get; set; } = CameraMode.Lock;
	public CameraState State { get; private set { field = value; } } = CameraState.Idle;
	private Layer<GroundLayer> _calibratingGroundLayer;
	
}
