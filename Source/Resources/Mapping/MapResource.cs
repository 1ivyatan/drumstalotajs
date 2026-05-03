using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Resources;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Resources.Mapping.Sets;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class MapResource : Resource
{
	[ExportGroup("Properties")]
	[Export] public double MetersPerPixel { get; set; } = 1;
	[Export] public double TimeLimitSecs { get; set; } = 60;
	
	[ExportGroup("Devices")]
	[Export] public Dictionary<Vector2I, double> DevicePositions { get; set; } = new();
	[Export] public Array<DeviceProps> DeviceProps { get; set; } = new();
	
	[ExportGroup("Layers")]
	[Export] public GroundLayerData GroundLayer { get; set; } = null;
	[Export] public AtlasLayerData DecorationLayer { get; set; } = null;
	[Export] public EntityLayerData EntityLayer { get; set; } = null;
	[Export] public OverlayLayerData OverlayLayer { get; set; } = null;
	
	public MapResource () {}
	public MapResource (Map map)
	{
		GroundLayer = map.GroundLayer.Export();
		DecorationLayer = map.DecorationLayer.Export();
		EntityLayer = map.EntityLayer.Export();
		OverlayLayer = map.OverlayLayer.Export();
	}
}
