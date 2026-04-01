using Godot;
using System;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class SceneTile : Resource
{
	[Export] public int Id { get; private set; }
	[Export] public string Name { get; private set; }
	[Export] public PackedScene Scene { get; private set; }
}
