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
	private bool _pressed = false;
	private bool _echo = false;
	//private SceneTile _paintingTile = false;
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventKey keyboardEvent)
		{
			_pressed = keyboardEvent.Pressed;
			_echo = keyboardEvent.Echo;
		} 
		
		if (@event is InputEventAction actionEvent)
		{
			if (@event.IsActionPressed("editor_next_overlay"))
			{
				if (_pressed && !_echo)
				{
					//_paintingTile = 
				}
				
			//	NextSceneTile(Map.OverlayLayer, Map.Selector.GetMousePositionTile());
			}
		}
	//	GD.Print($"{_pressed} - {_echo}");
	}
	
	private void NextSceneTile(SceneLayer layer, Vector2I cellPosition)
	{
		Vector2 localPosCentered = layer.MapToLocal(cellPosition);
		var tiles = layer.Flash(localPosCentered, 1);
		
		GD.Print(tiles);
	}
}
