using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene
{
	public partial class Battle : Node
	{
		[Export] public Resources.Levels.Level Level { get; private set; }
		private Map.MapWidget map;
		private Stages.StageManager stageManager;
		
		public override void _Ready()
		{
			map = GetNode<Node2D>("Map") as Map.MapWidget;
			stageManager = GetNode<Control>("UIOverlay/StageManager") as Stages.StageManager;
			map.LoadLayers(Level);
		}
		
		public void AssignLevel(Resources.Levels.Level level)
		{
			Level = level;
		}
	}
}
