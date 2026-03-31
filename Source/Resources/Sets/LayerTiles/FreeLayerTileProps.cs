using Godot;
using System;

namespace Drumstalotajs.Resources.Sets.Layers;

public partial class FreeLayerTileProps : Resource
{
	[Export] public int Id { get; set; }
	[Export] public Texture2D Thumbnail { get; set; }
	[Export] public PackedScene Scene { get; set; }
}
