using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Components.Modals;

namespace Drumstalotajs.Editor.Components;

public abstract partial class Props : Control
{
	public void Close()
	{
		Visible = false;
	}
	public abstract void Load(Vector2I position);
}
