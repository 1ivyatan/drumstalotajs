using Godot;
using System;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Mapping.Tiles.Overlays;

namespace Drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	
	private Vector2I _currentPosition;
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (Mode == SelectorMode.Locked) return;
		
		if (@event is InputEventMouse mouseEvent)
		{
			if (mouseEvent is InputEventMouseMotion mouseMotion) HandleMovement(mouseMotion);
			if (mouseEvent is InputEventMouseButton mouseClick) HandleClick(mouseClick);
		}
	}
	
	private void HandleMovement(InputEventMouseMotion inputEventMouse)
	{
		Vector2I position = GetMousePositionTile();
		if (_currentPosition != position)
		{
			_currentPosition = position;
			EmitSignal(SignalName.HoveredTile, _currentPosition);
		}
	}
	
	private void HandleClick(InputEventMouseButton inputEventMouse)
	{
		if (inputEventMouse.Pressed)
		{
			Flash(GetMousePosition());
		}
	}
	
}
