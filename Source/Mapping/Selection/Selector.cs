using Godot;
using System;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles.Overlays;

namespace Drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	[Signal] public delegate void PressedOverlayEventHandler(OverlayTile tile);
	[Signal] public delegate void PressedEmptyEventHandler();
	
	public SelectorFilter Filter { get; set; }
	private Map map { get; set; }
	
	public override void _Ready()
	{
		map = GetParent() as Map;
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouse mouse)
		{
			if (mouse is InputEventMouseButton mouseClick)
			{
				if (mouseClick.Pressed)
				{
					Flash(GetLocalMousePosition());
				}
			}
		}
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
}/*
namespace Drumstalotajs.Mapping.Layers;

public partial class GroundLayer : Layer*/
