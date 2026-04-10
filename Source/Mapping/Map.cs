using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Cameras;

namespace Drumstalotajs.Mapping;

public partial class Map : Node2D
{
	[Export] public GroundLayer GroundLayer { get; private set; }
	[Export] public Camera Camera { get; private set; }
}
