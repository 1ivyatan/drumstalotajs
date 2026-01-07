using Godot;
using System;

public partial class DeviceAdjustmentPanel : PanelContainer
{
	private Label infoBox;
	
	public override void _Ready()
	{
		this.infoBox = this.GetNode<Label>("VBoxContainer/InfoBox");
	}
	
	public void HideDeviceInfo()
	{
		this.Visible = false;
	}
	
	public void ShowDeviceInfo(Device device)
	{
		this.infoBox.Text = $"Azimuts: {device.Azimuth}°";
		this.Visible = true;
	}
}
