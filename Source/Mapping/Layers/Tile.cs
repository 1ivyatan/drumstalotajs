using Godot;
using System;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Mapping.Layers;

public abstract partial class Tile : Node2D
{
	public Vector2I CellPosition { get; protected set; }
}
