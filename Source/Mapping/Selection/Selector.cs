using Godot;
using System;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles.Overlays;

namespace Drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	public SelectorFilter Filter { get; set; }
	public SelectorMode Mode { get; set; } = SelectorMode.Locked;
	private Map _map { get; set; }
	
	public override void _Ready()
	{
		_map = GetParent() as Map;
	}
	
	public void Flash(Vector2 localPosition)
	{
		bool flashed = false;
		foreach (Layer layer in Filter.Layers)
		{
			switch (layer)
			{
				case OverlayLayer:
					var tiles = (layer as SceneLayer).Flash(localPosition, 9);
					if (tiles.Length > 0)
					{
						EmitSignal(SignalName.PressedOverlay, tiles[0]);
						flashed = true;
					}
					break;
				default: break;
			}
		}
		
		if (!flashed)
		{
			EmitSignal(SignalName.PressedEmpty);
		}
	}
}
