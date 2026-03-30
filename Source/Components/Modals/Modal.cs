using Godot;
using System;

namespace Drumstalotajs.Components.Modals;

public partial class Modal : Control
{
	private Control _window;
	private ColorRect _tint;
	
	public override void _Ready()
	{
		_window = GetNode<Control>("Window");
		_tint = GetNode<ColorRect>("Tint");
		foreach (var node in GetChildren())
		{
			if (node != _window && node != _tint)
			{
				node.Reparent(_window);
			}
		}
	}
	
	public void ShowModal()
	{
		Visible = true;
	}
	
	public void HideModal()
	{
		Visible = false;
	}
}
