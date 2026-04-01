using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers.Overlays;

namespace Drumstalotajs.Mapping;

public partial class Map : Node2D
{
	public OverlayLayer OverlayLayer { get; private set; }
	
	public override void _Ready()
	{
		OverlayLayer = GetNode("OverlayLayer") as OverlayLayer;
	}
}
