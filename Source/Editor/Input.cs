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
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouse mouseEvent)
		{
			if (mouseEvent is InputEventMouseButton mouseButton)
			{
				_mouseLeftPressed = mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left;
				_mouseRightPressed = mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Right;
			}
			
			if (_selectedTileData != null)
			{
				bool isTile = Types.ValidVector2I(_selectedTileData.Name);
				
				if (_mouseRightPressed)
				{
					if (isTile) Map.AddTile(_selectedTileData.Layer, Map.MouseToCellPosition(),  Types.StringToVector2I(_selectedTileData.Name));
					else Map.AddSceneTile((_selectedTileData.Layer as SceneLayer), Map.MouseToCellPosition(), _selectedTileData.Name);
				} else if (_mouseLeftPressed)
				{
					if (isTile) Map.RemoveTile(_selectedTileData.Layer, Map.MouseToCellPosition());
					else Map.RemoveSceneTile((_selectedTileData.Layer as SceneLayer), Map.MouseToCellPosition());
				}
			}
		}
	}
	
	private void NextSceneTile(SceneLayer layer, Vector2I cellPosition)
	{
		Vector2 localPosCentered = layer.MapToLocal(cellPosition);
		var tiles = layer.Flash(localPosCentered, 1);
		
		GD.Print(tiles);
	}
}
