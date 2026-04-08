using Godot;
using System;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Editor.TileEditing;

public partial class TileEditContainer : Control
{
	private Map _map;
	
	public override void _Ready()
	{
		_map = Nodes.GetSceneRoot().Map;
	}
	
	public void Open(Vector2I position)
	{
		_map = Nodes.GetSceneRoot().Map;
		if (_map.IsEmptyTile(position)) return;
		Visible = true;
	}
	
	public void Close()
	{
		
		Visible = false;
	}
}
