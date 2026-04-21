using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Components.Modals;

public partial class Modal : Window
{
	public override void _Ready()
	{
		PopupWindow = true;
		Exclusive = true;
		Transient = true;
		TransientToFocused = true;
		Unresizable = true;
	}

	public void OpenCentered()
	{
		PopupCentered();
		GrabFocus();
	}
}
