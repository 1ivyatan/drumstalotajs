using Godot;
using System;
using System.Threading.Tasks;
using Drumstalotajs;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Mapping;
using Drumstalotajs.Utilities;
using System.IO;

namespace Drumstalotajs.Editor.Components;

public partial class EditorSaveManager : Node
{
	[Signal] public delegate void SavedEventHandler();
	[Signal] public delegate void ChangedEventHandler();
	[Signal] public delegate void LoadedEventHandler();
	[Signal] public delegate void LoadedEventHandler();
	
	[ExportGroup("Nodes")]
	[Export] private Map _map;
	[Export] private FileDialog _openDialog;
	[Export] private FileDialog _saveDialog;
	[Export] private ConfirmationDialog _editedDialog;
	
	[ExportGroup("Files")]
	[Export] private string FileFormat = ".tres";
	[Export(PropertyHint.File, "*.tres,*.res")] public string TemplateMap { get; private set; }
	[Export] public string SaveName { get; set; } = "Untitled";
	
	public bool Edited { get; private set; } = false;
	private SaveFileMode _mode = SaveFileMode.None;
	
	public override void _Ready()
	{
		_map.Edited += () => {
			Edited = true;
			EmitSignal(SignalName.Changed);
		};
		
		_editedDialog.Confirmed += () => {
			switch (_mode)
			{
				case SaveFileMode.New: New(); break;
				default: break;
			}
			_mode = SaveFileMode.None;
		};
		
		_editedDialog.Canceled += () => {
			_mode = SaveFileMode.None;
		};
		
		//_saveDialog.FileSelected += (string path) => { AttemptSave(path); };
		//_openDialog.FileSelected += (string path) => { AttemptOpen(path); };
	}
	
	private void OpenConfirmAction(SaveFileMode mode)
	{
		_mode = mode;
		_editedDialog.PopupCentered();
	}
	
	public void AttemptNew()
	{
		if (Edited) OpenConfirmAction(SaveFileMode.New);
		else New();
	}
	
	private void New()
	{
		
	}
	
	/*private async Task ConfirmAction()
	{
		_confirm = false;
		await ToSignal(_editedDialog, AcceptDialog.SignalName.CloseRequested);
		GD.Print(_confirm);
		GD.Print("here");
	} */
/*
	public void OpenDialog()
	{
		_openDialog.PopupCentered();
	}
	
	public void SaveDialog()
	{
		_saveDialog.PopupCentered();
	}
	
	private void AttemptSave(string path)
	{
		var export = _map.Export();
		var editedPath = ProjectSettings.LocalizePath((path + ( !path.Contains(FileFormat) ? ".tres" : "" )).Replace("\\", "/"));
		ResourceSaver.Save(export, editedPath);
		Nodes.GetRoot().ToastManager.Spawn($"Done exporting, file is {Path.GetFileName(editedPath)}");
		EmitSignal(SignalName.Saved, editedPath);
	}
	
	private void AttemptOpen(string path)
	{
		
	}*/
}
