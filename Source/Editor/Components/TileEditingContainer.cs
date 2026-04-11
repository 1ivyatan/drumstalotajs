using Godot;
using System;
using Drumstalotajs.Editor;
using Drumstalotajs;

namespace Drumstalotajs.Editor.Components;

public partial class TileEditingContainer : Control
{
	public override void _Ready()
	{
		
	}
	
	public void Load(Vector2I position)
	{
		
		Visible = true;
	}
	
	public void Close()
	{
		Visible = false;
	}
}
