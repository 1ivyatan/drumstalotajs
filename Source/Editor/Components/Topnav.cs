using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Editor.Components;

public partial class Topnav : Drumstalotajs.Components.Panels.Topnav
{
	[Signal] public delegate void SelectedExitEventHandler();
	[Export] private MenuBar _menu;
	private PopupMenu _fileMenu;
	private PopupMenu _viewMenu;
	private Callable _fileMenuCall;
	private Callable _fileViewCall;
	
	public override void _Ready()
	{
		Initialize();
	}
	
	public override void _ExitTree()
	{
		Cleanup();
	}
	
	private void Initialize()
	{
		_fileMenu = _menu.GetNode<PopupMenu>("File");
		_viewMenu = _menu.GetNode<PopupMenu>("View");
		
		var viewModeMenu = new PopupMenu();
		viewModeMenu.Name = "Mode";
		viewModeMenu.AddRadioCheckItem("View");
		viewModeMenu.AddRadioCheckItem("Insert");
		viewModeMenu.AddRadioCheckItem("Edit");
		_viewMenu.AddChild(viewModeMenu);
		_viewMenu.AddSubmenuNodeItem("Mode", viewModeMenu);
		
		_fileMenuCall = Callable.From((int id) => {
			switch (id)
			{
				case 2: /* exit */
					EmitSignal(SignalName.SelectedExit);
					break;
				default: break;
			}
		});
		_fileViewCall = Callable.From((int id) => {
			
		});
		_fileMenu.Connect(PopupMenu.SignalName.IdPressed, _fileMenuCall);
		_viewMenu.Connect(PopupMenu.SignalName.IdPressed, _fileViewCall);
	}
	
	private void Cleanup()
	{
		_fileMenu.Disconnect(PopupMenu.SignalName.IdPressed, _fileMenuCall);
		_viewMenu.Disconnect(PopupMenu.SignalName.IdPressed, _fileViewCall);
	}
}
