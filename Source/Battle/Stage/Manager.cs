using Godot;
using System;

namespace Drumstalotajs.Battle.Stage
{
	public partial class Manager : Control
	{
		private Node CurrentStage { get; set; }
		
		public void DevicePlacement()
		{
			LoadStage("DevicePlacement");
		}
		
		public void DeviceAdjustment()
		{
			LoadStage("DeviceAdjustment");
		}
		
		public void Firing()
		{
			LoadStage("Firing");
		}
		
		private void LoadStage(string name)
		{
			string stagePath = $"res://Scenes/Battle/StageOverlays/{name}/{name}.tscn";
		
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
