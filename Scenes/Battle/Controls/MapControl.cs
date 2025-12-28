using Godot;
using System;

public partial class MapControl : Control
{
	private StageManager stageManager;
	
	public override void _Ready()
	{
		stageManager = GetNode<Control>("StageManager") as StageManager;
		
		stageManager.LoadStage("DevicePlacement");
	}
}
