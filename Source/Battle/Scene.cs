using Godot;
using System;

namespace Drumstalotajs.Battle
{	
	public partial class Scene : Control
	{
		public Resources.Levels.Level Level { get; private set; }
		
		private Stage.Manager _stageManager;
		private Map.Widget _map;
		
		public override void _Ready()
		{
			_stageManager = GetNode<Control>("MapContainer/StageOverlay") as Stage.Manager;
			_map = GetNode<Node2D>("MapContainer/Map") as Map.Widget;
			_map.LoadLayers(Level.GroundPatternPath, Level.DecorationPatternPath, Level.EntityPatternPath);
			_stageManager.DevicePlacement();
		}
		
		public void AssignLevel(Resources.Levels.Level level)
		{
			Level = level;
		}
		
		public void AssignLevel(string name)
		{
			string levelResourcePath = $"res://Resources/Levels/{name}.tres";
			
			if (ResourceLoader.Exists(levelResourcePath))
			{
				Level = ResourceLoader.Load<Resources.Levels.Level>(levelResourcePath);
			} else
			{
				Level = null;
			}
		}
	}
}
