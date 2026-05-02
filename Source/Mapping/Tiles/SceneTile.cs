using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Tiles;

public partial class SceneTile : Tile
{
	public int TileId { get; set; } = -1;
	public virtual Dictionary Data { get; set; } = new();

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
