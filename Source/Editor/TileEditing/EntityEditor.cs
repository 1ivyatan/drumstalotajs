using Godot;
using System;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Editor.TileEditing;

public partial class EntityEditor : Control
{
	private Map _map;
	
	public override void _Ready()
	{
		_map = Nodes.GetMap();
		
		
		GD.Print(_map);
	}
}
