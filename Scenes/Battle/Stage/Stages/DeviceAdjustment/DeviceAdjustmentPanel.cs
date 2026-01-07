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
	
	public void ShowDeviceInfo(Device device, Vector2I position)
	{
		this.infoBox.Text = $"Novietojums: {position}\nAzimuts: {device.Azimuth}";
		this.Visible = true;
	}
}
