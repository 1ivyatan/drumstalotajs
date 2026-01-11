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
	
	public void StartBattle()
	{
		this.stageManager.LoadStage("Battle");
	}
	
	public void LeaveBattle()
	{
		this.EmitSignal(SignalName.LevelSelect);
	}
}
