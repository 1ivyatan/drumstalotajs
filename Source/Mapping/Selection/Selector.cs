using Godot;
using System;

namespace Drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	[Export] private Map _map;
	
	public void GetTiles(Vector2I position)
	{
		//if (_map.GroundLayer.GetCellAtlasCoords(position) == )
	}
}
