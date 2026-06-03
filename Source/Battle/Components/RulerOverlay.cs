using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Battle.Components;

public partial class RulerOverlay : Control
{
	[Export] private Map _map;
	[Export] private Button _deselect;
	[Export] private Button _wipe;
	[Export] private Button _removeNewest;
	
	private bool _adding = false;
	private Vector2 _pointA;
	private Vector2 _pointB;
	
	public override void _Ready()
	{
		_deselect.Pressed += () => { _adding = false; };
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
						var pos = _map.ViewportMouseToLocal();
						if (!_adding)
						{
							_adding = true;
							_pointA = pos;
							GD.Print($"1. {pos}");
						} else
						{
							_pointB = pos;
							_adding = false;
							GD.Print($"2. {pos}");
						}
					} else
					{
						_adding = false;
					}
				}
			}
		}
	}
}
