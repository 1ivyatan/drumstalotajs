using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Mapping.Layers;

public partial class SceneLayer : Layer
{
	[Signal] public delegate void SpawnedTileEventHandler(SceneTile tile);
	
	public List<SceneTile> Instances { get; private set; }
	private Resources.Layers.SceneLayer sceneLayerSet;
	
	public override void _Ready()
	{
		sceneLayerSet = TileSet as Resources.Layers.SceneLayer;
		sceneLayerSet.PrepareAtlas();
		Instances = new List<SceneTile>();
		ChildEnteredTree += (Node node) => {
			if (node is SceneTile tile)
			{
				Instances.Add(tile);
				EmitSignal(SignalName.SpawnedTile, tile);
			}
		};
		ChildExitingTree += (Node node) => {
			if (node is SceneTile tile)
			{
				Instances.Remove(tile);
			}
		};
	}
	
	public void AddTile(string name, Vector2I position)
	{
		int id = sceneLayerSet.SceneTiles.FirstOrDefault(s => s.Name == name).Id;
		SetCell(position, 0, new Vector2I(0, 0), id);
	}
	
	public SceneTile AddTile(int id, Vector2I position)
	{
		SetCell(position, 0, new Vector2I(0, 0), id);
		return Instances.FirstOrDefault(i => position == LocalToMap(position));
	}
	
	public SceneTile GetTile(Vector2 position)
	{
		return Instances.FirstOrDefault(i => position == LocalToMap(position));
	}
	
	public SceneTile[] Flash(Vector2 localPosition, int limit)
	{
		var spaceState = GetWorld2D().DirectSpaceState;
		PhysicsPointQueryParameters2D query = new PhysicsPointQueryParameters2D();
		query.Position = GlobalPosition + localPosition;
		query.CollideWithAreas = true;
		var intersectedNodes = spaceState.IntersectPoint(query, limit);
		if (intersectedNodes.Count > 0)
		{
			List<SceneTile> tiles = new List<SceneTile>(); 
			foreach (var node in intersectedNodes)
			{
				Node2D collider = (Node2D)node["collider"];
				if (collider is SceneTile tile)
				{
					tiles.Add(tile);
				}
			}
			if (tiles.Count > 0)
			{
				return tiles.OrderBy(tile => {
					return localPosition.DistanceTo(tile.Position);
				}).ToArray();
			} else
			{
				return [];
			}
		}
		return [];
	}
	/*
		var spaceState = GetWorld2D().DirectSpaceState;
		PhysicsPointQueryParameters2D query = new PhysicsPointQueryParameters2D();
		query.Position = GlobalPosition + localPos;
		query.CollideWithAreas = true;
		var intersectedNodes = spaceState.IntersectPoint(query, limit);
		if (intersectedNodes.Count > 0)
		{
			List<Entities.Entity> entities = new List<Entities.Entity>(); 
			foreach (var node in intersectedNodes)
			{
				Node2D collider = (Node2D)node["collider"];
				if (collider is Entities.Entity entity)
				{
					entities.Add(entity);
				}
			}
			
			if (entities.Count > 0)
			{
				return entities.OrderBy(entity => {
					return localPos.DistanceTo(entity.Position);
				}).ToArray();
			} else
			{
				return [];
			}
		}
		
		return [];*/
}
