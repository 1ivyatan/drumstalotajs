using Godot;
using System;

namespace Drumstalotajs.Battle
{	
	public partial class Scene : Node
	{
		private Stages.Manager _stageManager;
		
		public override void _Ready()
		{
			_stageManager = GetNode<Control>("MapContainer/StageOverlay") as Stages.Manager;
			
			_stageManager.DevicePlacement();
		}
		
		public void LoadLevel(string name)
		{
			string levelResourcePath = $"res://Resources/Levels/{name}.tres";
			
			if (ResourceLoader.Exists(levelResourcePath))
			{
				Drumstalotajs.Resources.Level levelResource = ResourceLoader.Load<Drumstalotajs.Resources.Level>(levelResourcePath);

				GD.Print(levelResource.Title);
			}
		}
	}
}
