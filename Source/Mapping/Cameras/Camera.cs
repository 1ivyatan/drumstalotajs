using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Cameras;

public partial class Camera : Camera2D
{
	[Export] private AtlasLayer _calibratingAtlasLayer;
	public CameraMode Mode { get; set; } = CameraMode.Locked;

	public override void _Ready()
	{
	}
}
