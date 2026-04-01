using Godot;
using System;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class MapMeta : Resource
{
	[Export] public string Name { get; private set; }
}
