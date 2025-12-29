using Godot;
using System;

public partial class TopPanel : PanelContainer
{
	private Button exitButton;
	private Label deviceCountLabel;
	private Label topbarLabel;
	
	public void SetDeviceCountLabel(string text)
	{
		this.deviceCountLabel.Text = text;
	}
	
	public void SetTopbarLabel(string text)
	{
		this.topbarLabel.Text = text;
	}
	
	public override void _Ready()
	{
		this.exitButton = this.GetNode<Button>("OptionsStatsGrid/ExitButton");
		this.deviceCountLabel = this.GetNode<Label>("OptionsStatsGrid/StatsContainer/DeviceCount");
		this.topbarLabel = this.GetNode<Label>("FlyingLabel");
	}
}
