using Godot;
using System;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class MapMeta : Resource
{
	[Export] public String Name { get; set; }
	[Export] public String MapDataPath { get; set; }
	
	public MapData LoadMapData()
	{
		return Files.SafeLoadResource<MapData>(MapDataPath);
	}
}
