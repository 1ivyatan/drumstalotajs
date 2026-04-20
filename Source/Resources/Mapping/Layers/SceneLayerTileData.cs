using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Resources.Mapping.Layers;

[GlobalClass]
public partial class SceneLayerTileData : Resource
{
	[Export] public int Id { get; set; }
	[Export] public Vector2I Position { get; set; }
}
