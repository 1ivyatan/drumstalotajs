using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Editor.Components;

public abstract partial class Props : Control
{
	public void Close()
	{
		Visible = false;
	}
	public abstract void Load(Tile tile);
}
