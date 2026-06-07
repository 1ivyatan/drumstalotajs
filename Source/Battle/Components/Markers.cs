using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Battle;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Mapping.Entities;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Resources.Mapping.Entities;
using Drumstalotajs.Mapping.Cameras;

namespace Drumstalotajs.Battle.Components;

public partial class Markers : CanvasLayer
{
	private Camera _camera;
	
	public override void _Ready()
	{
	}
}
