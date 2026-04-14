using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class AtlasLayerData : LayerData
{
	[Export] public Vector2I Offset { get; private set; }
	[Export] public TileMapPattern Tiles { get; private set; }
	
	public AtlasLayerData(AtlasLayer layer)
	{
		
	}
}
