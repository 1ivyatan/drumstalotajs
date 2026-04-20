using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping;

public partial class Map : Node2D
{
	[Export] public OverlayLayer OverlayLayer { get; private set; }
	
	public override void _Ready()
	{
		OverlayLayer.AddTile(new Vector2I(5, 5), "LevelMarker");
	}
}
