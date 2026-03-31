using Godot;
using System;
using Drumstalotajs.Resources.Sets.Layers;
using Drumstalotajs.Resources.Mapping;
using Drumstalotajs.Resources.Mapping.FreeLayers;

namespace Drumstalotajs.Mapping.Layers;

public abstract partial class FreeLayer : Node2D, ISaveableLayer
{
	[Export] private FreeLayerTileSet TileSet { get; set; }
	[Export] public FreeLayerData LayerData { get; set; }
	
	public void Load(FreeLayerData layerData)
	{
		GD.Print(layerData.FreeTiles);
	}
	
	public void AddFreeTile(int id)
	{
		
	}
}
