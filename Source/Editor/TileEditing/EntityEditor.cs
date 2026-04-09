using Godot;
using System;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Editor.TileEditing;

public partial class EntityEditor : Control//, ITileEditorWindow<SceneTile>
{
	private Map _map;
	
	public override void _Ready()
	{
		_map = Nodes.GetMap();
		
		
	}
}
