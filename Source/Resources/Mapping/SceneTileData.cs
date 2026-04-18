using Godot;
using System;
using Godot.Collections;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class SceneTileData : Resource
{
	[Export] public int Id { get; private set; }
	[Export] public string Name { get; private set; }
	[Export] public Texture2D Thumbnail { get; private set; }
	[Export] public PackedScene Scene { get; private set; }
}
