using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Components.Modals;

public partial class Modal : Control
{
	[Signal] public delegate void ExitingEventHandler();
	[Export] public string Title { get; 
		set {
			field = value;
			_window.Title = field;
		}
	} = "Modal";
	[Export] private ColorRect _tint;
	[Export] private Window _window;
	
	public override void _Ready()
	{
		foreach (var node in GetChildren())
		{
			if (node != _window && node != _tint)
			{
				node.Reparent(_window);
			}
		}
		_window.Title = Title;
		_window.CloseRequested += () => {
			EmitSignal(SignalName.Exiting);
		};
	}
	
	public void Popup()
	{
		Visible = true;
		_window.PopupCentered();
		_window.GrabFocus();
	}
	
	public void Close()
	{
		Visible = false;
		_window.Hide();
	}
}
