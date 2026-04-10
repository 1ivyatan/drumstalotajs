using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Cameras;

namespace Drumstalotajs.Mapping;

public partial class Map : Node2D
{
	public MapMode Mode { get; set; } = MapMode.Lock;
	public MapState State { get; set; } = MapState.Empty;
	[Export] public GroundLayer GroundLayer { get; private set; }
	[Export] public Camera Camera { get; private set; }
	
	public override void _Ready()
	{
		/* vvvvv */
	}
}
