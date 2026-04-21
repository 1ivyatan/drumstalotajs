using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Mapping;
using Drumstalotajs.Utilities;
using System.IO;

namespace Drumstalotajs.Editor.Components;

public partial class EditorSaveManager : Node
{
	[Signal] public delegate void SavedEventHandler(string filename);
	[Signal] public delegate void LoadedEventHandler(string filename);
	[Export] private string fileFormat = ".tres";
	[Export] private Map _map;
	[Export] private FileDialog _openDialog;
	[Export] private FileDialog _saveDialog;
	
	public override void _Ready()
	{
		_saveDialog.FileSelected += (string path) => { AttemptSave(path); };
		_openDialog.FileSelected += (string path) => { AttemptOpen(path); };
	}

	public void OpenDialog(string path)
	{
		_openDialog.PopupCentered();
	}
	
	public void SaveDialog(string path)
	{
		_saveDialog.PopupCentered();
	}
	
	private void AttemptSave(string path)
	{
		var export = _map.Export();
		var editedPath = ProjectSettings.LocalizePath((path + ( !path.Contains(fileFormat) ? ".tres" : "" )).Replace("\\", "/"));
		ResourceSaver.Save(export, editedPath);
		Nodes.GetRoot().ToastManager.Spawn($"Done exporting, file is {Path.GetFileName(editedPath)}");
		EmitSignal(SignalName.Saved, editedPath);
	}
	
	private void AttemptOpen(string path)
	{
		
	}
}
