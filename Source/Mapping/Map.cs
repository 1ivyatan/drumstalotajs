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
	//	OverlayLayer.AddTile(new Vector2I(5, 5), "LevelMarker");
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		/*
		if (@event is InputEventMouseButton)
		{
			var h = OverlayLayer.Flash(new Vector2I(5, 5));
			
			if (h.Count > 0)
			{
				GD.Print(h[0].cat);
			}
		}*/
	}
}
