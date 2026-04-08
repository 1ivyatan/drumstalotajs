using Godot;
using System;
using Drumstalotajs.Mapping;
using Drumstalotajs.Utils;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Mapping.Tiles.Overlays;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Resources.Progress;

namespace Drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	private bool _mouseLeftPressed = false;
	private bool _mouseRightPressed = false;
	private SelectedTileData _selectedTileData = null;
	
	public async override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("editor_to_insert"))
		{
			Mode = EditMode.Insert;
		} else if (@event.IsActionPressed("editor_to_edit"))
		{
			Mode = EditMode.Edit;
		}
		
		if (@event is InputEventMouse mouseEvent)
		{
			if (mouseEvent is InputEventMouseButton mouseButton)
			{
				_mouseLeftPressed = mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left;
				_mouseRightPressed = mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Right;
			}
		}
		
		Vector2I pos = Map.Selector.GetMousePositionTile();
		
		switch (Mode)
		{
			case EditMode.Edit:
				
				if (_mouseRightPressed)
				{
					_tileEditContainer.Open(pos);
				} else if (_mouseLeftPressed)
				{
					_tileEditContainer.Close();
				}
				break;
			case EditMode.Insert:
				if (_selectedTileData != null)
				{
					bool isTile = Types.ValidVector2I(_selectedTileData.Name);
					if (_mouseRightPressed)
					{
						if (isTile) _selectedTileData.Layer.AddTile(
							pos,
							Types.StringToVector2I(_selectedTileData.Name)
						);
						else await (_selectedTileData.Layer as SceneLayer).AddTile(
							pos, _selectedTileData.Name
						);
					} else if (_mouseLeftPressed)
					{
						_selectedTileData.Layer.RemoveTile(pos);
					}
				}
				break;
			default: break;
		}
	}
}
