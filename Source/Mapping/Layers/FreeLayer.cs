using Godot;
using System;
using System.Collections.Generic;
using Drumstalotajs.Resources.Sets.Layers;
using Drumstalotajs.Resources.Mapping;
using Drumstalotajs.Resources.Mapping.FreeLayers;

namespace Drumstalotajs.Mapping.Layers;

public abstract partial class FreeLayer : Node2D, ISaveableLayer
{
	[Export] private FreeLayerTileSet TileSet { get; set; }
	[Export] public FreeLayerData LayerData { get; set; }
	
	public List<FreeTile> Instances { get; private set; }
	
	public override void _Ready()
	{
		Instances = new List<FreeTile>();
		ChildEnteredTree += (Node node) => {
			if (node is FreeTile)
			{
				Instances.Add(node as FreeTile);
			}
		};
		ChildExitingTree += (Node node) => {
			if (node is FreeTile)
			{
				Instances.Remove(node as FreeTile);
			}
		};
	}
	
	public void Load(FreeLayerData layerData)
	{
		foreach (var node in GetChildren())
		{
			
		}
		
		foreach (var freeTile in layerData.FreeTiles)
		{
			SpawnTile(freeTile.Id, freeTile.Position);
			GD.Print(freeTile.Id);
		}
	}
	
	public FreeTile SpawnTile(int id, Vector2 position)
	{
		FreeTile tile = TileSet.GetTileProps(id).Instantiate();
		tile.Initialize(id, position);
		AddChild(tile);
		return tile;
	}
	
	public void DestroyTile(FreeTile tile)
	{
		tile.QueueFree();
		RemoveChild(tile);
	}
}
