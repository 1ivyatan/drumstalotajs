using Godot;
using System;

public partial class DevicePlacingHeader : Widget
{
	Label addedCount;
	Button exitButton;
	
	protected override void LoadWidget()
	{
		addedCount = GetNode<Label>("PanelContainer/HFlowContainer/CenterContainer/AddedCount");
		exitButton = GetNode<Button>("PanelContainer/HFlowContainer/ExitButton");
		
		exitButton.Connect("pressed", new Callable(this, nameof(ExitBattle)));
	}
	
	void ExitBattle()
	{
		(root as Battle).LeaveBattle();
	}
	
	public void SetLabels(int deviceCount)
	{
		switch (deviceCount)
		{
			case 0:
				addedCount.Text = "Nav nekas ievietots";
				break;
			case 1:
				addedCount.Text = "1 ievietots";
				break;
			default:
				addedCount.Text = $"{deviceCount} ievietoti";
				break;
		}
	}
}
