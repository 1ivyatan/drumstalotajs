using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Mapping;

public partial class Map : Node2D
{
	[Export] public OverlayLayer OverlayLayer { get; private set; }
	[Export] public EntityLayer EntityLayer { get; private set; }
	
	public override void _Ready()
	{
		var test = new OverlayLayerTileData();
		test.Id = 1;
		test.Radians = 1;
		test.Position = new Vector2I(5, 5);
		OverlayLayer.AddTile(test);
		/*
	public async void AddTile(SceneLayerTileData atlas)*/
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
