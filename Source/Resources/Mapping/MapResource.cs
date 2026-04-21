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
	
	public MapResource () {}
	public MapResource (Map map)
	{
		
	}
}
