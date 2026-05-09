using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Battle.Stages;

public partial class StageManager : CanvasLayer
{
	[Export] private Dictionary<string, string> Stages { get; set; }
	public Node CurrentStage { get; private set; }
	
	public override void _Ready()
	{
		
	}
	
	private void SetStage(string path)
	{
		if (CurrentStage != null)
		{
			CurrentStage.QueueFree();
			RemoveChild(CurrentStage);
		}
			
		if (ResourceLoader.Exists(path))
		{
			CurrentStage = Files.SafeLoadResource<PackedScene>(path).Instantiate();
			AddChild(CurrentStage);
		}
	}
	
	public void DevicePlacement() { SetStage(Stages["DevicePlacement"]); }
	public void InitDeviceAdjustment() { SetStage(Stages["InitDeviceAdjustment"]); }
	public void Unmasking() { SetStage(Stages["Unmasking"]);}
	public void Firing(FiringMode mode) {
		SetStage(Stages["Firing"]);
		((Firing)CurrentStage).StartFiring(mode);
	}
	public void DeviceAdjustment() { SetStage(Stages["DeviceAdjustment"]); }
	public void End() { SetStage(Stages["End"]); }
}
