using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Resources;
using Drumstalotajs.Resources.Mapping.Layers;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class MapResource : Resource
{
	[ExportGroup("Layers")]
	[Export] GroundLayerData GroundLayer { get; set; } = null;
	[Export] AtlasLayerData DecorationLayer { get; set; } = null;
	[Export] EntityLayerData EntityLayer { get; set; } = null;
	[Export] OverlayLayerData OverlayLayer { get; set; } = null;

	public MapResource () {}
	public MapResource (Map map)
	{
		GroundLayer = map.GroundLayer.Export();
		DecorationLayer = map.DecorationLayer.Export();
		EntityLayer = map.EntityLayer.Export();
		OverlayLayer = map.OverlayLayer.Export();
	}
}
