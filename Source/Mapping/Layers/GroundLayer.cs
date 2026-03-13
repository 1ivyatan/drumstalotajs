using Godot;
using System;

namespace drumstalotajs.Mapping.Layers;

public partial class GroundLayer : Layer
{
	public int TileSize { get => TileSet.TileSize.X; }
	
	public override void _Ready()
	{
		
	}
}
