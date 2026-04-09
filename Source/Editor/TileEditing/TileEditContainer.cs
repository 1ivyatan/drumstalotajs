using Godot;
using System;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;

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
		
		GD.Print(_map.EntityLayer.AsISceneLayer.Flash(_map.Selector.GetMousePosition(), 9));
		GD.Print(_map.OverlayLayer.AsISceneLayer.Flash(_map.Selector.GetMousePosition(), 9));
		Visible = true;
	}
	
	public void Close()
	{
		
		Visible = false;
	}
}
