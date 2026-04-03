using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Battle.Stages;

public partial class StageManager : Node
{
	[Export] private string StagesPath;
	private Node CurrentStage { get; set; }
	
	public void DevicePlacement()
	{
		SetStage("DevicePlacement");
	}
	
	private void SetStage(string name)
	{
		string stagePath = $"{StagesPath}/{name}.tscn";
		
		if (CurrentStage != null)
		{
			CurrentStage.QueueFree();
			RemoveChild(CurrentStage);
		}
			
		if (ResourceLoader.Exists(stagePath))
		{
			CurrentStage = Files.SafeLoadResource<PackedScene>(stagePath).Instantiate();
			AddChild(CurrentStage);
		}
	}
	
}
