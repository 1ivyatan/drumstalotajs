using Godot;
using System;
using Drumstalotajs.Editor;
using Drumstalotajs;

namespace Drumstalotajs.Editor.Components;

public partial class Topnav : Drumstalotajs.Components.Panels.Topnav
{
	[Signal] public delegate void SelectedExitEventHandler();
	[Signal] public delegate void SelectedModeEventHandler(EditorMode mode);
	[Export] private MenuBar _menu;
	private PopupMenu _fileMenu;
	private PopupMenu _viewMenu;
	private Callable _fileMenuCall;
	private Callable _viewMenuModeCall;
	
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
		viewModeMenu.AddRadioCheckItem("View", (int)EditorMode.View);
		viewModeMenu.AddRadioCheckItem("Insert", (int)EditorMode.Insert);
		viewModeMenu.AddRadioCheckItem("Edit", (int)EditorMode.Edit);
		viewModeMenu.SetItemChecked((int)EditorMode.View, true);
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
		_viewMenuModeCall = Callable.From((int id) => {
			foreach (var mode in Enum.GetValues(typeof(EditorMode)))
			{
				if (viewModeMenu.IsItemChecked((int)mode))
				{
					viewModeMenu.SetItemChecked((int)mode, false);
					break;
				}
			}
			viewModeMenu.SetItemChecked((int)id, true);
			EmitSignal(SignalName.SelectedMode, id);
		});
		_fileMenu.Connect(PopupMenu.SignalName.IdPressed, _fileMenuCall);
		viewModeMenu.Connect(PopupMenu.SignalName.IdPressed, _viewMenuModeCall);
	}
	
	private void Cleanup()
	{
		_fileMenu.Disconnect(PopupMenu.SignalName.IdPressed, _fileMenuCall);
		_viewMenu.Disconnect(PopupMenu.SignalName.IdPressed, _viewMenuModeCall);
	}
}
