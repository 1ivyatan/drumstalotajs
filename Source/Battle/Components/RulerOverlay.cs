using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Battle.Components;

public partial class RulerOverlay : Control
{
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
				if (mouseButton.ButtonIndex == MouseButton.Right)
				{
					GD.Print(222);
				}
				/*
				mouseButton.ButtonIndex == MouseButton.Left &&*/
			}
		}
	}
}
