using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Editor;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Editor.Components;

public partial class TileEditingContainer : Control
{
	[Export] private Map _map;
	
	public override void _Ready()
	{
		
	}
	
	public void Load(Vector2I position)
	{
		_map.Selector.GetTiles(position);
		Visible = true;
	}
	
	public void Close()
	{
		Visible = false;
	}
}
