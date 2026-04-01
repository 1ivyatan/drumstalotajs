using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Mapping.Layers;

public partial class SceneTile : Area2D
{
	[Export] public Resources.Mapping.SceneTile Resource { get; private set; }
}
