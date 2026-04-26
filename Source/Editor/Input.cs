using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor.Components;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Mapping.Cameras;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	private bool _mouseLeftPressed = false;
	private bool _mouseRightPressed = false;
	private bool _mouseMoving = false;
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouse mouseEvent)
		{
			if (mouseEvent is InputEventMouseButton mouseButton)
			{
				_mouseLeftPressed = mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left;
				_mouseRightPressed = mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Right;
			}
		
			if (mouseEvent is InputEventMouseMotion mouseMotion)
			{
				_mouseMoving = true;
			} else
			{
				_mouseMoving = false;
			}
		}
		
		switch (Mode)
		{
			case EditorMode.Insert: HandleInsert(@event); break;
			default: break;
		}
	}
	
	private void HandleInsert(InputEvent @event)
	{
		if ((_mouseLeftPressed || _mouseRightPressed) && InsertWindow.PickedTile == null)
		{
			Nodes.GetRoot().ToastManager.SpawnOne("Pick a tile");
			return;
		}
		
		if (_mouseLeftPressed)
		{
			Map.AddTile(
				InsertWindow.PickedTile.Layer,
				InsertWindow.PickedTile.Atlas,
				Map.ViewportMouseToMap()
			);
		} else if (_mouseRightPressed)
		{
			Map.RemoveTile(
				InsertWindow.PickedTile.Layer,
				Map.ViewportMouseToMap()
			);
		}
	}
}
