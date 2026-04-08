using Godot;
using System;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Editor.TileEditing;

public partial class TileEditContainer : Control
{
	private Map _map;
	private GroundEditor _groundEditor;
	private EntityEditor _entityEditor;
	
	public override void _Ready()
	{
		_map = Nodes.GetSceneRoot().Map;
		_groundEditor = GetNode("VBoxContainer/GroundEditor") as GroundEditor;
		_entityEditor = GetNode("VBoxContainer/EntityEditor") as EntityEditor;
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
