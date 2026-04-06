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
	//private bool _mouseMoving = false;
	//private SceneTimer _mouseDelayTimer;
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouse mouseEvent)
		{
			if (mouseEvent is InputEventMouseMotion mouseMotion)
			{
				
			}
			//bool pressed = (mouseEvent is InputEventMouseButton mouseButton && //mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left);
			
			//GD.Print(_mouseMoving);
			//GD.Print(pressed);
		}
		//Map.Selector.GetMousePosition();
	}
}
