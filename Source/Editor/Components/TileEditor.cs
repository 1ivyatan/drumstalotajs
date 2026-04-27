using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Components.Modals;

namespace Drumstalotajs.Editor.Components;

public partial class TileEditor : Control
{
	[Export] private Map _map;
	
	public override void _Ready()
	{
	}
	
	public void Load(Vector2I coords)
	{
		Visible = true;
	}
	
	public void Close()
	{
		Visible = false;
	}
}
