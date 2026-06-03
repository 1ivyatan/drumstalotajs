using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Battle.Components;

public partial class RulerOverlay : Control
{
	[Export] private Map _map;
	private bool _adding = false;
	private Vector2 _pointA;
	private Vector2 _pointB;
	
	public override void _Ready()
	{
	}
	
	public override void _Input(InputEvent @event)
	{
		if (!Visible) return;
		
		if (@event is InputEventMouse mouseEvent)
		{
			bool moving = false;
			
			if (mouseEvent is InputEventMouseMotion mouseMotion)
			{
				moving = true;
			}
			
			if (mouseEvent is InputEventMouseButton mouseButton && 
				mouseButton.Pressed && !moving
			)
			{
				bool rClick = mouseButton.ButtonIndex == MouseButton.Right;
				bool lClick = mouseButton.ButtonIndex == MouseButton.Left;
				
				if (rClick)
				{
					if (_map.ViewportMouseOnMap())
					{
						
					} else
					{
						
					}
					
				}
			}
		}
	}
}
