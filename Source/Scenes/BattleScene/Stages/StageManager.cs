using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Stages
{
	public partial class StageManager : Control
	{
		private Map.MapWidget map;
		
		public override void _Ready()
		{
			map = GetNode<Node2D>("../../Map") as Map.MapWidget;
			
		/*	map.DraggingChange += (Map.MapWidget.DraggingState state) => {
				if (state != Map.MapWidget.DraggingState.NONE)
				{
					SetDefaultCursorShape(Control.CursorShape.Move);
				} else {
					SetDefaultCursorShape(Control.CursorShape.Arrow);
				}
			};*/
		}
	}
}
