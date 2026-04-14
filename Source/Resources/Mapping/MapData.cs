using Godot;
using System;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class MapData : Resource
{
	[ExportGroup("AtlasLayers")]
	[Export] public GroundLayerData GroundLayerData { get; private set; }
	
	public MapData(GroundLayerData groundLayerData)
	{
		GroundLayerData = groundLayerData;
	}
}
