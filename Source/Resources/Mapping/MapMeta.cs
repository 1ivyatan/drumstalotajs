using Godot;
using System;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class MapMeta : Resource
{
	[Export] public string Name { get; private set; }
	[Export] public string Desc { get; private set; }
	[Export] public string PathToMap { get; private set; }
}
