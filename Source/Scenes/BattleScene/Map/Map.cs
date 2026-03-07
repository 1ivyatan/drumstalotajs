using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene
{
	public partial class Map : Node2D
	{
		private bool dragging = false;
		
		public override void _Input(InputEvent @event)
		{
			if (@event is InputEventMouse mouseEvent)
			{
				if (mouseEvent is InputEventMouseButton mouseClick)
				{
					dragging = mouseClick.Pressed && mouseClick.ButtonIndex == (MouseButton)1;
				}
				
				if (mouseEvent is InputEventMouseMotion mouseMotion)
				{
					if (dragging)
					{
						GD.Print(mouseMotion.Relative);
					}
				}
			}
		}
	}
}
