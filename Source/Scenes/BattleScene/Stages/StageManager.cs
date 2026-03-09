using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Stages
{
	public partial class StageManager : Control
	{
		private Node CurrentStage { get; set; }
		private Map.MapWidget map;
		
		public void DevicePlacement()
		{
			LoadStage("Placement");
		}
		
		public override void _Ready()
		{
			map = GetNode<Node2D>("../../Map") as Map.MapWidget;
			SetDefaultCursorShape(Control.CursorShape.Cross);
			DevicePlacement();
		/*	map.DraggingChange += (Map.MapWidget.DraggingState state) => {
				if (state != Map.MapWidget.DraggingState.NONE)
				{
					SetDefaultCursorShape(Control.CursorShape.Move);
				} else {
					SetDefaultCursorShape(Control.CursorShape.Arrow);
				}
			};*/
		}
		
		private void LoadStage(string name)
		{
			string stagePath = $"res://Scenes/Bat/Stages/{name}.tscn";
		
			if (CurrentStage != null)
			{
				CurrentStage.QueueFree();
				RemoveChild(CurrentStage);
			}
			
			if (ResourceLoader.Exists(stagePath))
			{
				CurrentStage = ResourceLoader.Load<PackedScene>(stagePath).Instantiate();
				AddChild(CurrentStage);
			}
		}
	}
}
