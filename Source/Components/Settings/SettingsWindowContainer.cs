using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Components.Settings;

public partial class SettingsWindowContainer : Control
{
	[Export] private Window _window;
	
	public override void _Ready()
	{
		_window.CloseRequested += CloseWindow;
	}
	
	public void OpenWindow()
	{
		_window.PopupCentered();
		Visible = true;
	}
	
	public void CloseWindow()
	{
		_window.Hide();
		Visible = false;
	}
}
