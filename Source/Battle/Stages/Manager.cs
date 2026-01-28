using Godot;
using System;

namespace Drumstalotajs.Battle.Stages
{
	public partial class Manager : Control
	{
		private Node CurrentStage { get; set; }
		
		public void DevicePlacement()
		{
			LoadStage("DevicePlacement");
		}
		
		private void LoadStage(string name)
		{
			string stagePath = $"res://Scenes/Battle/Stages/{name}.tscn";
		
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
