using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
using Drumstalotajs.Resources.Sets.Layers;
using Drumstalotajs.Resources.Mapping;
using Drumstalotajs.Resources.Mapping.FreeLayers;

namespace Drumstalotajs.Mapping.Layers;

public abstract partial class FreeLayer : Node2D, ISaveableLayer
{
	[Signal] public delegate void ClickedTileEventHandler(Vector2 position, FreeTile tile);
	[Signal] public delegate void ClickedEmptyTileEventHandler(FreeTile tile);
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
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouse mouseEvent)
		{
			if (mouseEvent is InputEventMouseButton mouseClick)
			{
				HandleClick(mouseClick);
			}
			
		}
	}
	
	private void HandleClick(InputEventMouseButton mouseClick)
	{
		if (mouseClick.Pressed)
		{
			/*!!!!!!!!!!!!!!*/
			Vector2 position = GetLocalMousePosition();
			FreeTile[] clickedTiles = Flash(position, 9);
			if (clickedTiles.Length > 0)
			{
				EmitSignal(SignalName.ClickedTile, position, clickedTiles[0]);
			} else
			{
				EmitSignal(SignalName.ClickedEmptyTile, position);
			}
		}
	}
	
	public void Load(FreeLayerData layerData)
	{
		foreach (var node in GetChildren())
		{
			DestroyTile(node as FreeTile);
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
	
	public FreeTile[] Flash(Vector2 position, int limit)
	{
		var spaceState = GetWorld2D().DirectSpaceState;
		PhysicsPointQueryParameters2D query = new PhysicsPointQueryParameters2D();
		query.Position = GlobalPosition + position;
		query.CollideWithAreas = true;
		var intersectedNodes = spaceState.IntersectPoint(query, limit);
		if (intersectedNodes.Count > 0)
		{
			List<FreeTile> freeTiles = new List<FreeTile>();
			foreach (var node in intersectedNodes)
			{
				Node2D collider = (Node2D)node["collider"];
				if (collider is FreeTile freeTile)
				{
					freeTiles.Add(freeTile);
				}
			}
			
			if (freeTiles.Count > 0)
			{
				return freeTiles.OrderBy(freeTile => {
					return position.DistanceTo(freeTile.Position);
				}).ToArray();
			} else
			{
				return [];
			}
		}
		
		return [];
	}
}
