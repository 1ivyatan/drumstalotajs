using Godot;
using System;

public partial class Battle : Control
{
	[Signal]
	public delegate void LevelSelectEventHandler();
	
	private StageManager stageManager;
	
	public override void _Ready()
	{
		this.stageManager = GetNode<Control>("MapControl/StageManager") as StageManager;
		this.stageManager.LoadStage("DevicePlacement");
	}
	
	public void StartDeviceAdjustment()
	{
		this.stageManager.LoadStage("DeviceAdjustment");
	}
	
	public void StartDeviceFiring()
	{
		this.stageManager.LoadStage("DeviceFiring");
	}
	
	public void LeaveStage()
	{
		this.EmitSignal(SignalName.LevelSelect);
	}
}
