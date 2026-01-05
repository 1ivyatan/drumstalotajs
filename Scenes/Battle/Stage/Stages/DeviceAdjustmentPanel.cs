using Godot;
using System;

public partial class DeviceAdjustmentPanel : PanelContainer
{
	public void HideInfo()
	{
		this.Visible = false;
	}
	
	public void ShowEntityInfo(Entity entity)
	{
		
		GD.Print(entity);
		this.Visible = true;
	}
}
