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
	public string SaveName { get; set; } = "Untitled";
	
	public override void _Ready()
	{
		_openDialog.FileSelected += Open;
		_saveDialog.FileSelected += Save;
	}
	
	private void Open(string path)
	{
		GD.Print(path);
		SaveName = Path.GetFileNameWithoutExtension(path);
		_map.Load(ProjectSettings.LocalizePath(path.Replace("\\", "/")));
		EmitSignal(SignalName.SaveLoaded, SaveName);
	}
	
	private void Save(string path)
	{
		var export = _map.Export();
		ResourceSaver.Save(export, ProjectSettings.LocalizePath(path.Replace("\\", "/")));
		SaveName = Path.GetFileNameWithoutExtension(path);
		Nodes.GetRoot().ToastManager.Spawn($"Done exporting, file is {Path.GetFileName(path)}");
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
}
