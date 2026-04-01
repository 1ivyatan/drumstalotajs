using Godot;
using System;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles.Overlays;

namespace Drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	[Signal] public delegate void PressedOverlayEventHandler(OverlayTile tile);
	
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
		foreach (Layer layer in Filter.Layers)
		{
			switch (layer)
			{
				case OverlayLayer:
					var tile = (layer as SceneLayer).Flash(localPosition, 9)[0];
					EmitSignal(SignalName.PressedOverlay, tile);
					break;
				default: break;
			}
		}
	}
}/*
namespace Drumstalotajs.Mapping.Layers;

public partial class GroundLayer : Layer*/
