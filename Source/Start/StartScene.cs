using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Start;

public partial class StartScene : Control
{
	[Export(PropertyHint.File, "*.txt")] private string _annotationFilePath;
	[Export] AcceptDialog _annotation;
	[Export] private Button _start;
	[Export] private Button _editor;
	[Export] private Button _about;

	public override void _Ready()
	{
		var annFile = Files.SafeLoadFile(_annotationFilePath,  FileAccess.ModeFlags.Read);
		if (annFile != null)
		{
			_annotation.DialogText = annFile.GetAsText();
		}
		
		_editor.Pressed += () => {
			Nodes.GetRoot().SceneManager.Editor();
		};
		
		_about.Pressed += () => {
			_annotation.PopupCentered();
		};
	}
}
