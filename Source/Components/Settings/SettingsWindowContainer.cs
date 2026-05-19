using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Components.Settings;

public partial class SettingsWindowContainer : Control
{
	[Export] private SettingsWindow _window;
	
	public override void _Ready()
	{
		_window.CloseRequested += CloseWindow;
	}
	
	public void OpenWindow()
	{
		_window.Visible = true;
		Visible = true;
	}
	
	public void CloseWindow()
	{
		_window.Visible = false;
		Visible = false;
	}
}
