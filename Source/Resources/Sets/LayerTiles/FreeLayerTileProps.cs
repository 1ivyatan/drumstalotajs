using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Resources.Sets.Layers;

[GlobalClass]
public partial class FreeLayerTileProps : Resource
{
	[Export] public int Id { get; set; }
	[Export] public Texture2D Thumbnail { get; set; }
	[Export] public PackedScene Scene { get; set; }
	
	public FreeTile Instantiate()
	{
		return Scene.Instantiate() as FreeTile;
	}
}
