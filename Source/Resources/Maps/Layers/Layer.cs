using Godot;
using System;
using Godot.Collections;

namespace drumstalotajs.Resources.Maps.Layers;

[GlobalClass]
public partial class Layer : Resource
{
	[Export] public Vector2I Offset { get; set; }
	[Export] public TileMapPattern Tiles { get; set; }
}
