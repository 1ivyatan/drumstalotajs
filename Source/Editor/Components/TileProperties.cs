using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Editor.Components;

public abstract partial class TileProperties : Control
{
	public abstract void Load(Tile tile);
	public abstract void Close();
}
