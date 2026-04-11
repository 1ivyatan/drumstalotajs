using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Editor.Components;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Cameras;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	private bool _mouseLeftPressed = false;
	private bool _mouseRightPressed = false;
	private bool _mouseMoving = false;
	
	public async override void _UnhandledInput(InputEvent @event)
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
			case EditorMode.Insert:
				if (_mouseLeftPressed)
				{
					Map.AddTile(
						_tileSelectionContainer.PickedTileData.Layer,
						_tileSelectionContainer.PickedTileData.Name,
						Map.GetCellPosFromMouse()
					);
				} else if (_mouseRightPressed)
				{
					Map.RemoveTile(
						_tileSelectionContainer.PickedTileData.Layer,
						Map.GetCellPosFromMouse()
					);
				}
				break;
			case EditorMode.Edit:
				if (_mouseMoving) return;
				if (_mouseRightPressed)
				{
					GD.Print("rclick");
				}
				
				break;
			default: break;
		}
	}
}
