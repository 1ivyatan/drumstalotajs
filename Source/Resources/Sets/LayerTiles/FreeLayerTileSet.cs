using Godot;
using System;
using System.Linq;

namespace Drumstalotajs.Resources.Sets.Layers;

[GlobalClass]
public partial class FreeLayerTileSet : Resource
{
	[Export] public int TileSize { get; set; }
	[Export] public FreeLayerTileProps[] TileSet { get; set; }
	
	public FreeLayerTileProps GetTileProps(int id)
	{
		return TileSet.FirstOrDefault(t => t.Id == id);
	}
}
