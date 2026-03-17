using Godot;
using System;

namespace drumstalotajs.Editor;

public partial class EditorContainer : PanelContainer
{
	public void ToggleContainer()
	{
		SetVisible(!Visible);
	}
}
