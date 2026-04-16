using Godot;
using System;
using Drumstalotajs.Editor;
using Drumstalotajs;

namespace Drumstalotajs.Editor.Components;

public partial class Topnav : Drumstalotajs.Components.Panels.Topnav
{
	[Signal] public delegate void SelectedExitEventHandler();
	[Signal] public delegate void SelectedCalibrationEventHandler();
	[Signal] public delegate void SelectedModeEventHandler(EditorMode mode);
	[Signal] public delegate void SelectedExportEventHandler();
	[Signal] public delegate void SelectedNewEventHandler();
	[Signal] public delegate void SelectedOpenEventHandler();
	[Export] private MenuBar _menu;
	private PopupMenu _fileMenu;
	private PopupMenu _viewMenu;
	private PopupMenu _viewMenuMode;
	private Callable _fileMenuCall;
	private Callable _viewMenuCall;
	private Callable _viewMenuModeCall;
	
	public override void _Ready()
	{
		Initialize();
	}
	
	public override void _ExitTree()
	{
		Cleanup();
	}
	
	public void SetMode(EditorMode newMode)
	{
		foreach (EditorMode mode in Enum.GetValues(typeof(EditorMode)))
		{
			if (!_viewMenuMode.IsItemChecked((int)mode) && newMode == mode)
			{
				_viewMenuMode.SetItemChecked((int)newMode, true);
				EmitSignal(SignalName.SelectedMode, (int)newMode);
			} else if (_viewMenuMode.IsItemChecked((int)mode) && newMode != mode)
			{
				_viewMenuMode.SetItemChecked((int)mode, false);
			} else continue;
		}
	}
	
	private void Initialize()
	{
		_fileMenu = _menu.GetNode<PopupMenu>("File");
		_viewMenu = _menu.GetNode<PopupMenu>("View");
		_viewMenuMode = new PopupMenu();
		_viewMenuMode.Name = "Mode";
		_viewMenuMode.AddRadioCheckItem("View", (int)EditorMode.View);
		_viewMenuMode.AddRadioCheckItem("Insert", (int)EditorMode.Insert);
		_viewMenuMode.AddRadioCheckItem("Edit", (int)EditorMode.Edit);
		_viewMenuMode.SetItemChecked((int)EditorMode.View, true);
		_viewMenu.AddChild(_viewMenuMode);
		_viewMenu.AddSubmenuNodeItem("Mode", _viewMenuMode);
		_fileMenuCall = Callable.From((int id) => {
			switch (id)
			{
				case 3: /* new */
					EmitSignal(SignalName.SelectedNew);
					break;
				case 4: /* open */
					EmitSignal(SignalName.SelectedOpen);
					break;
				case 0: /* export */
					EmitSignal(SignalName.SelectedExport);
					break;
				case 2: /* exit */
					EmitSignal(SignalName.SelectedExit);
					break;
				default: break;
			}
		});
		_viewMenuCall = Callable.From((int id) => {
			switch (id)
			{
				case 0: /* calibrate */
					EmitSignal(SignalName.SelectedCalibration);
					break;
				default: break;
			}
		});
		_viewMenuModeCall = Callable.From((int id) => {
			foreach (var mode in Enum.GetValues(typeof(EditorMode)))
			{
				if (_viewMenuMode.IsItemChecked((int)mode))
				{
					_viewMenuMode.SetItemChecked((int)mode, false);
					break;
				}
			}
			_viewMenuMode.SetItemChecked((int)id, true);
			EmitSignal(SignalName.SelectedMode, id);
		});
		_fileMenu.Connect(PopupMenu.SignalName.IdPressed, _fileMenuCall);
		_viewMenu.Connect(PopupMenu.SignalName.IdPressed, _viewMenuCall);
		_viewMenuMode.Connect(PopupMenu.SignalName.IdPressed, _viewMenuModeCall);
	}
	
	private void Cleanup()
	{
		_fileMenu.Disconnect(PopupMenu.SignalName.IdPressed, _fileMenuCall);
		_viewMenu.Disconnect(PopupMenu.SignalName.IdPressed, _viewMenuCall);
		_viewMenuMode.Disconnect(PopupMenu.SignalName.IdPressed, _viewMenuModeCall);
	}
}
