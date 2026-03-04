using Godot;
using System;

namespace Drumstalotajs.Battle
{	
	public partial class Scene : Control
	{
		public Resources.Levels.Level Level { get; private set; }
		
		private Stage.Manager _stageManager;
		
		public override void _Ready()
		{
			_stageManager = GetNode<Control>("MapContainer/StageOverlay") as Stage.Manager;
			
			_stageManager.DevicePlacement();
		}
		
		public void LoadLevel(Resources.Levels.Level level)
		{
			Level = level;
		}
		
		public void LoadLevel(string name)
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
