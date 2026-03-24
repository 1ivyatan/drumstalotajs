using Godot;
using System;

namespace drumstalotajs.Components.Modals;

public partial class Window : Control
{
	private Modal modal;
	
	public override void _Ready()
	{
		modal = GetNode<Control>("..") as Modal;
	}
	
	public Modal GetModal()
	{
		return modal;
	}
}
