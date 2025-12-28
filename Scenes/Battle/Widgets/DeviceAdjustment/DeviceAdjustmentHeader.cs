using Godot;
using System;

public partial class DeviceAdjustmentHeader : Widget
{
	Button exitButton;
	
	protected override void LoadWidget()
	{
		exitButton = GetNode<Button>("PanelContainer/HFlowContainer/ExitButton");
		exitButton.Connect("pressed", new Callable(this, nameof(ExitBattle)));
	}
	
	void ExitBattle()
	{
		(root as Battle).LeaveBattle();
	}
}
