using Godot;
using System;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class MapData : Resource
{
	[ExportGroup("AtlasLayers")]
	[Export] public GroundLayerData GroundLayerData { get; private set; }
	
	public MapData(Map map)
	{
		GroundLayerData = map.GroundLayer.Export();
	}
}
