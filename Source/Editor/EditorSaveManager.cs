using Godot;
using System;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Cameras;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Utils;
using Drumstalotajs.Resources.Mapping;
using System.IO;

namespace Drumstalotajs.Editor;

public partial class EditorSaveManager : Node
{
	[Signal] public delegate void SaveLoadedEventHandler(string filename);
	
	[Export] private string fileFormat = ".tres";
	[Export(PropertyHint.File, "*.tres")] public string TemplateMap { get; private set; }
	[Export] private FileDialog _openDialog;
	[Export] private FileDialog _saveDialog;
	[Export] private Map _map;
	[Export] public string SaveName { get; set; } = "Untitled";
	public bool Edited { get; private set; } = false;
	
	public override void _Ready()
	{
		_map.Edited += Edit;
		_openDialog.FileSelected += Open;
		_saveDialog.FileSelected += Save;
	}
	
	private void Edit()
	{
		Edited = true;
	}
	
	private void Open(string path)
	{
		SaveName = Path.GetFileNameWithoutExtension(path);
		_map.Load(ProjectSettings.LocalizePath(path.Replace("\\", "/")));
		Edited = false;
		EmitSignal(SignalName.SaveLoaded, SaveName);
	}
	
	private void Save(string path)
	{
		var export = _map.Export();
		var editedPath = ProjectSettings.LocalizePath((path + ( !path.Contains(fileFormat) ? ".tres" : "" )).Replace("\\", "/"));
		ResourceSaver.Save(export, editedPath);
		SaveName = Path.GetFileNameWithoutExtension(editedPath);
		Edited = false;
		Nodes.GetRoot().ToastManager.Spawn($"Done exporting, file is {Path.GetFileName(editedPath)}");
		EmitSignal(SignalName.SaveLoaded, SaveName);
	}
	
	public void OpenPrompt()
	{
		_openDialog.PopupCentered();
	}
	
	public void SavePrompt()
	{
		_saveDialog.PopupCentered();
	}
	
	public void OpenNew()
	{
		_map.Load(TemplateMap);
		Edited = false;
		EmitSignal(SignalName.SaveLoaded, SaveName);
	}
}
