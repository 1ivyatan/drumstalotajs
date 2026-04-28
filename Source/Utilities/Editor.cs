using Godot;
using System;

namespace Drumstalotajs.Utilities;

public static class Editor
{
	public static bool IsEditor()
	{
		return OS.HasFeature("editor");
	}
	
	public static void EditorControl(Control control)
	{
		bool editor = IsEditor();
		control.Visible = editor;
		
		if (control is BaseButton button && !editor)
		{
			button.Disabled = true;
		}
	}
}
