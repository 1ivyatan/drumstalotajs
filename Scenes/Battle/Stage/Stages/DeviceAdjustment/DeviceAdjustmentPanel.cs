using Godot;
using System;

public partial class DeviceAdjustmentPanel : PanelContainer
{
	public void HideDeviceInfo()
	{
		this.Visible = false;
	}
	
	public void ShowDeviceInfo(Entity entity)
	{
		
		GD.Print(entity);
		this.Visible = true;
	}
}
