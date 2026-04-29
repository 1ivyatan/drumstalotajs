using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Components;
using Drumstalotajs.Editor.Components;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Mapping.Cameras;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.LevelSelection.Components;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.LevelSelection;

public partial class LevelSelectionScene : Node2D
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

		if (_mouseMoving) return;
		
		if (_mouseLeftPressed)
		{
			var tiles = Map.Flash(Map.ViewportMouseToMap());
			if (tiles.Count > 0 && 
				tiles.ContainsKey(Map.OverlayLayer) &&
				tiles[Map.OverlayLayer].Count > 0 )
			{
				LevelMetaContainer.Load((OverlayTile)tiles[Map.OverlayLayer][0]);
			} else
			{
				LevelMetaContainer.Close();
			}
		}
	}
}
