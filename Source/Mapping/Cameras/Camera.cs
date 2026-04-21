using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Mapping.Cameras;

public partial class Camera : Camera2D
{
	public CameraMode Mode { get; set; } = CameraMode.Locked;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}
