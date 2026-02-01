using Godot;
using System;

namespace Drumstalotajs.Battle
{	
	public partial class Scene : Control
	{
		public Drumstalotajs.Resources.Level Level { get; private set; }
		
		private Stage.Manager _stageManager;
		
		public override void _Ready()
		{
			_stageManager = GetNode<Control>("MapContainer/StageOverlay") as Stage.Manager;
			
			_stageManager.DevicePlacement();
		}
		
		public void LoadLevel(string name)
		{
			string levelResourcePath = $"res://Resources/Levels/{name}.tres";
			
			if (ResourceLoader.Exists(levelResourcePath))
			{
				Level = ResourceLoader.Load<Drumstalotajs.Resources.Level>(levelResourcePath);

				GD.Print(Level.Title);
			} else
			{
				Level = null;
			}
		}
	}
}
