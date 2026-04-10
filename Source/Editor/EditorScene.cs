using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Editor.Components;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Cameras;

namespace Drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	[Export] private Topnav _topnav;
	[Export] public Map Map;
	
	public override void _Ready()
	{
		_topnav.SetTitle("Editor");
		Map.Camera.SetCalibratingAtlasLayer(Map.GroundLayer);
		Map.Camera.Mode = CameraMode.View;
		Map.Camera.Calibrate();
	}
}
