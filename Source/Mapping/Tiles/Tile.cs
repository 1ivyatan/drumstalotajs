using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Mapping.Tiles;

public partial class Tile : Node2D
{
	public Vector2I CellPosition { get; protected set; }
	
	public Tile(Vector2I cellPosition)
	{
		CellPosition = cellPosition;
	}
}
