using Godot;
using System;

namespace drumstalotajs.Battle.Stages;

public partial class StageManager : CanvasLayer
{
	[Export] private string StagePath;
	
	private Node CurrentStage { get; set; }
	
	public void DevicePlacementStage()
	{
		SetStage("DevicePlacement");
	}
	
	private void SetStage(string name)
	{
		string stagePath = $"{StagePath}/{name}.tscn";
		
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
