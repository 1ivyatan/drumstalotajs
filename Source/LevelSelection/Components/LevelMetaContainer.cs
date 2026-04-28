using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.LevelSelection.Components;

public partial class LevelMetaContainer : Control
{
	[Export] private Button _loadCustomMap;
	[Export] private FileDialog _customMapDialog;
	
	public override void _Ready()
	{
		Utilities.Editor.EditorControl(_loadCustomMap);
		if (Utilities.Editor.IsEditor())
		{
			_customMapDialog.FileSelected += (string path) => {
				GD.Print(path);
			};
			_loadCustomMap.Pressed += () => {
				_customMapDialog.PopupCentered();
			};
		}
	}
}
