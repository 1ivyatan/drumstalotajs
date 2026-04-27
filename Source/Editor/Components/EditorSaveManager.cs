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
	
	[ExportGroup("Nodes")]
	[Export] private Map _map;
	[Export] private FileDialog _openDialog;
	[Export] private FileDialog _saveDialog;
	[Export] private FileDialog _exportDialog;
	[Export] private ConfirmationDialog _editedDialog;
	
	[ExportGroup("Files")]
	[Export] private string ExportFormat = ".res";
	[Export] private string SaveFormat = ".tres";
	[Export(PropertyHint.File, "*.tres,*.res")] public string TemplateMap { get; private set; }
	[Export] public string SaveName { get; set; } = "Untitled";
	public string Path { get; private set; } = "";
	
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
				case SaveFileMode.Open:  _openDialog.PopupCentered(); break;
				default: break;
			}
			_mode = SaveFileMode.None;
		};
		
		_editedDialog.Canceled += () => {
			_mode = SaveFileMode.None;
		};
		
		_saveDialog.FileSelected += (string path) => { Save(path); };
		_openDialog.FileSelected += (string path) => { Open(path); };
		_exportDialog.FileSelected += (string path) => { Export(path); };
	}
	
	private void OpenConfirmAction(SaveFileMode mode)
	{
		_mode = mode;
		_editedDialog.PopupCentered();
	}
	
	private void ResetProps(string path, string editedPath)
	{
		SaveName = System.IO.Path.GetFileNameWithoutExtension(path);
		Path = editedPath;
		Edited = false;
	}
	
	public void AttemptNew()
	{
		if (Edited) OpenConfirmAction(SaveFileMode.New);
		else New();
	}
	
	public void AttemptSave()
	{
		if (Path.Length == 0)
		{
			AttemptSaveAs();
		} else Save(Path);
	}
	
	public void AttemptExport()
	{
		_exportDialog.PopupCentered();
	}
	
	public void AttemptSaveAs()
	{
		_saveDialog.PopupCentered();
	}
	
	public void AttemptOpen()
	{
		if (Edited) OpenConfirmAction(SaveFileMode.Open);
		else _openDialog.PopupCentered();
	}
	
	public void Export(string path)
	{
		var editedPath = ProjectSettings.LocalizePath((path + ( !path.Contains(ExportFormat) ? ".tres" : "" )).Replace("\\", "/"));
		var export = _map.Export();
		ResourceSaver.Save(export, editedPath, 
			ResourceSaver.SaverFlags.Compress
		);
		Nodes.GetRoot().ToastManager.Spawn("Exported!");
	}
	
	public void Save(string path)
	{
		var editedPath = ProjectSettings.LocalizePath((path + ( !path.Contains(SaveFormat) ? ".tres" : "" )).Replace("\\", "/"));
		var export = _map.Export();
		ResourceSaver.Save(export, editedPath//, 
		//	ResourceSaver.SaverFlags.ChangePath
		);
		ResetProps(path, editedPath);
		Nodes.GetRoot().ToastManager.Spawn("Saved!");
		EmitSignal(SignalName.Saved);
	}
	
	public void Open(string path)
	{
		var editedPath = ProjectSettings.LocalizePath(path.Replace("\\", "/"));
		_map.Load(editedPath);
		ResetProps(path, editedPath);
		EmitSignal(SignalName.Loaded);
	}
	
	public void New()
	{
		Open(TemplateMap);
		Path = "";
	}
}
