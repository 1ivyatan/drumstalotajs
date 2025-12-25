using Godot;
using System;

public partial class DevicePlacingHeader : Widget
{
	Label addedCount;
	
	protected override void LoadWidget()
	{
		addedCount = GetNode<Label>("HFlowContainer/AddedCount");
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
