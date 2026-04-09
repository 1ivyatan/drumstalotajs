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
		_map = Nodes.GetMap();
		_groundEditor = GetNode("VBoxContainer/GroundEditor") as GroundEditor;
		_entityEditor = GetNode("VBoxContainer/EntityEditor") as EntityEditor;
	}
	
	public void Open(Vector2I position)
	{
		if (_map.IsEmptyTile(position))
		{
			Close();
			return;
		}
		_groundEditor.Load(position);
		
		
		Visible = true;
	}
	
	public void Close()
	{
		
		Visible = false;
	}
}
