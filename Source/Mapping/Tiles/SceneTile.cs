using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Tiles;

public partial class SceneTile : Tile
{
	protected void Die()
	{
		if (GetParent() is TileMapLayer layer)
		{
			layer.EraseCell(layer.LocalToMap(Position));
		} else
		{
			QueueFree();
			GetParent().RemoveChild(this);
		}
	}
}
