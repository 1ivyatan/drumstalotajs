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
	[Export] private Button _exit;

	public override void _Ready()
	{
		Utilities.Editor.EditorControl(_editor);
		
		var annFile = Files.SafeLoadFile(_annotationFilePath,  FileAccess.ModeFlags.Read);
		if (annFile != null)
		{
			_annotation.DialogText = annFile.GetAsText();
		}
		
		_start.Pressed += () => {
			Nodes.GetRoot().SceneManager.LevelSelection();
		};
		
		_editor.Pressed += () => {
			if (Utilities.Editor.IsEditor())
			{
				Nodes.GetRoot().SceneManager.Editor();
			}
		};
		
		_about.Pressed += () => {
			_annotation.PopupCentered();
		};
		
		_exit.Pressed += () => {
			Nodes.GetRoot().Exit();
		};
	}
}
