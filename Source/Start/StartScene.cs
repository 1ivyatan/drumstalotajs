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
		if (!OS.HasFeature("editor"))
		{
			_editor.Visible = false;
			_editor.Disabled = true;
		} else
		{
			_editor.Visible = true;
			_editor.Disabled = false;
		}
		
		var annFile = Files.SafeLoadFile(_annotationFilePath,  FileAccess.ModeFlags.Read);
		if (annFile != null)
		{
			_annotation.DialogText = annFile.GetAsText();
		}
		
		_editor.Pressed += () => {
			if (OS.HasFeature("editor"))
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
