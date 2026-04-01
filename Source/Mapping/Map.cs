using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Selection;

namespace Drumstalotajs.Mapping;

public partial class Map : Node2D
{
	public MapMode Mode { get; set; } = MapMode.Locked;
	public OverlayLayer OverlayLayer { get; private set; }
	public Selector Selector { get; private set; }
	
	public override void _Ready()
	{
		OverlayLayer = GetNode("OverlayLayer") as OverlayLayer;
		Selector = GetNode("Selector") as Selector;
	}
}
