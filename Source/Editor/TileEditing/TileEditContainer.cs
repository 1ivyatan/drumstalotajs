using Godot;
using System;

namespace Drumstalotajs.Editor.TileEditing;

public partial class TileEditContainer : Control
{
	public void Open(Vector2I position)
	{
		
		Visible = true;
	}
	
	public void Close()
	{
		
		Visible = false;
	}
}
