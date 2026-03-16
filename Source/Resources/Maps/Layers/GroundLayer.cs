using Godot;
using System;
using Godot.Collections;

namespace drumstalotajs.Resources.Maps.Layers;

[GlobalClass]
public partial class GroundLayer : Layer
{
	[Export] public Dictionary<Vector2I, double> RelativeHeights { get; set; }
}
