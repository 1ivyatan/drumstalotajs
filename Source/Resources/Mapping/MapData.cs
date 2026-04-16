using Godot;
using System;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class MapData : Resource
{
	[ExportGroup("AtlasLayers")]
	[Export] public GroundLayerData GroundLayerData { get; set; } = null;
	[Export] public DecorationLayerData DecorationLayerData { get; set; } = null;
	
	public MapData() {}
	
	public MapData(Map map)
	{
		GroundLayerData = map.GroundLayer.Export();
		DecorationLayerData = map.DecorationLayer.Export();
	}
}
