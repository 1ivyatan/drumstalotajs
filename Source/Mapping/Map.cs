using Godot;
using System;
using Drumstalotajs.Resources.Mapping;
using Drumstalotajs.Mapping.Layers.Overlays;

namespace Drumstalotajs.Mapping;

public partial class Map : Node2D
{
	[Export] private MapMeta MapMeta { get; set; }
	public OverlayLayer OverlayLayer { get; private set; }
	
	public override void _Ready()
	{
		OverlayLayer = GetNode("OverlayLayer") as OverlayLayer;
	}
	
	public void LoadMap(MapMeta mapMeta)
	{
		MapMeta = mapMeta;
		GD.Print(mapMeta);
	}
}
