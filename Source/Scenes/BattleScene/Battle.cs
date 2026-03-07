using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene
{
	public partial class Battle : Node
	{
		private Map.MapWidget map;
		private Stages.StageManager stageManager;
		
		public override void _Ready()
		{
			map = GetNode<Node2D>("Map") as Map.MapWidget;
			stageManager = GetNode<Control>("UI/StageManager") as Stages.StageManager;
		}
		
		public override void _Input(InputEvent @event)
		{
			if (@event is InputEventMouse mouseEvent)
			{
				if (mouseEvent is InputEventMouseButton mouseClick)
				{
					map.Dragging = mouseClick.Pressed && mouseClick.ButtonIndex == (MouseButton)1;

					if (map.Dragging)
					{
						stageManager.MouseDefaultCursorShape = Control.CursorShape.Move;
					} else
					{
						stageManager.MouseDefaultCursorShape = Control.CursorShape.Arrow;
					}
				}
			}
		}
	}
}
