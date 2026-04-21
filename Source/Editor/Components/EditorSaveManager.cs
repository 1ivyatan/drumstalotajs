using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Editor.Components;

public partial class EditorSaveManager : Node
{
	[Signal] public delegate void SavedEventHandler(string filename);
	[Signal] public delegate void LoadedEventHandler(string filename);
	[Export] private Map _map;

	public async void AttemptOpen(string path)
	{
		
	}
	
	public void AttemptSave(string path)
	{
		GD.Print();
	}
}
