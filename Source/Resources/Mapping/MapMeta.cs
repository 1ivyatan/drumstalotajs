using Godot;
using System;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class MapMeta : Resource
{
	[Export] public string Name { get; private set; }
	[Export] public string Desc { get; private set; }
	[Export] public string PathToMap { get; private set; }
	
	public Map LoadMap()
	{
		return Files.SafeLoadResource<Map>(PathToMap);
	}
}
