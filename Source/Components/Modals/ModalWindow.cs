using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Components.Modals;

public partial class ModalWindow : Control
{
	protected void RequestClose()
	{
		var parent = GetParent();
		if (parent is Window window)
		{
			window.EmitSignal(Window.SignalName.CloseRequested);
		}
	}
}
