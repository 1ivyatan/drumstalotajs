using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Components.Settings;

public partial class SettingsWindowContainer : Control
{
	public override void _Ready()
	{
	}
	
	public void OpenWindow()
	{
		
		Visible = true;
	}
	
	public void CloseWindow()
	{
		Visible = false;
	}
}
