using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;

namespace Drumstalotajs.Editor.Components;

public partial class EditorTopnav : Topnav
{
	[Signal] public delegate void SelectedNewEventHandler();
	[Signal] public delegate void SelectedOpenEventHandler();
	[Signal] public delegate void SelectedSaveEventHandler();
	[Signal] public delegate void SelectedSaveAsEventHandler();
	[Signal] public delegate void SelectedCloseEventHandler();
	[Signal] public delegate void SelectedCameraCalibrateEventHandler();
	[Signal] public delegate void SelectedModeEventHandler(EditorMode mode);
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
				/* new */ case 0: EmitSignal(SignalName.SelectedNew); break;
				/* open */ case 1: EmitSignal(SignalName.SelectedOpen); break;
				/* save */ case 2: EmitSignal(SignalName.SelectedSave); break;
				/* save as */ case 7: EmitSignal(SignalName.SelectedSaveAs); break;
				/* close */ case 6: EmitSignal(SignalName.SelectedClose); break;
				default: break;
			}
		});
		_viewMenuCall = Callable.From((int id) => {
			switch (id)
			{
				/* calibrate camera */ case 0: EmitSignal(SignalName.SelectedCameraCalibrate); break;
				default: break;
			}
		});
		_viewMenuModeCall = Callable.From((int id) => {
			EmitSignal(SignalName.SelectedMode, (int)id);
		});
		_fileMenu.Connect(PopupMenu.SignalName.IdPressed, _fileMenuCall);
		_viewModeMenu.Connect(PopupMenu.SignalName.IdPressed, _viewMenuModeCall);
		_viewMenu.Connect(PopupMenu.SignalName.IdPressed, _viewMenuCall);
	}
	
	public void SetModeMarking(EditorMode id)
	{
		foreach (var mode in Enum.GetValues(typeof(EditorMode)))
		{
			if (_viewModeMenu.IsItemChecked((int)mode))
			{
				_viewModeMenu.SetItemChecked((int)mode, false);
				break;
			}
		}
		_viewModeMenu.SetItemChecked((int)id, true);
	}
}
