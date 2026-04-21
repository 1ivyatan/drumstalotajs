using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;

namespace Drumstalotajs.Editor.Components;

public partial class EditorTopnav : Topnav
{
	[Signal] public delegate void SelectedSaveEventHandler();
	[Export] private PopupMenu _fileMenu;
	[Export] private PopupMenu _viewMenu;
	private PopupMenu _viewModeMenu;
	private Callable _fileMenuCall;
	private Callable _viewMenuCall;
	private Callable _viewMenuModeCall;
	
	public override void _Ready()
	{
		_viewModeMenu = new PopupMenu();
		_viewModeMenu.Name = "Mode";
		_viewModeMenu.AddRadioCheckItem("View", (int)EditorMode.View);
		_viewModeMenu.AddRadioCheckItem("Insert", (int)EditorMode.Insert);
		_viewModeMenu.AddRadioCheckItem("Edit", (int)EditorMode.Edit);
		_viewMenu.AddChild(_viewModeMenu);
		_viewMenu.AddSubmenuNodeItem("Mode", _viewModeMenu);
		_fileMenuCall = Callable.From((int id) => {
			switch (id)
			{
				/* save */
				case 2:
					EmitSignal(SignalName.SelectedSave);
					break;
			}
		});
		_viewMenuCall = Callable.From((int id) => {
			
		});
		_viewMenuModeCall = Callable.From((int id) => {
			
		});
		_fileMenu.Connect(PopupMenu.SignalName.IdPressed, _fileMenuCall);
		_viewMenu.Connect(PopupMenu.SignalName.IdPressed, _viewMenuCall);
		_viewModeMenu.Connect(PopupMenu.SignalName.IdPressed, _viewMenuModeCall);
	}
}
