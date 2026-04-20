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
		var test = new EntityLayerTileData();
		test.Id = 1;
		test.Azimuth = 1;
		test.Position = new Vector2I(5, 5);
		EntityLayer.AddTile(test);
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton)
		{
			var ins = EntityLayer.GetInstance(new Vector2I(5, 5));
			if (ins != null)
			{
				ins.DecreaseIntegrity(10);
				GD.Print(ins.Integrity);
			}
		}
	}
}
