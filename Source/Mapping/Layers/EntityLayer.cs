using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

namespace drumstalotajs.Mapping.Layers;

public partial class EntityLayer : Node2D
{
	[Export] public Resources.Sets.EntitySet EntitySetResource { get; set; }
	public OrderedDictionary<int, PackedScene> EntityScenes { get; private set; }
	public List<Entities.Entity> Entities { get; private set; }
	public int TileSize { get; private set; }
	
	public override void _Ready()
	{
		EntityScenes = new OrderedDictionary<int, PackedScene>();
		Entities = new List<Entities.Entity>();
		TileSize = EntitySetResource.TileSize;
		
		foreach (var scene in EntitySetResource.Scenes)
		{
			EntityScenes.Add(scene.Key, scene.Value);
		}
		
		ChildEnteredTree += EntitySpawned;
		ChildExitingTree += EntityExited;
	}
	
	private void EntitySpawned(Node node)
	{
		if (node is Entities.Entity)
		{
			Entities.Add(node as Entities.Entity);
		}
	}
	
	private void EntityExited(Node node)
	{
		if (node is Entities.Entity)
		{
			Entities.Remove(node as Entities.Entity);
		}
	}
	
	public Entities.Entity SpawnEntity(Vector2 position, int id)
	{
		Entities.Entity entity = EntityScenes[id].Instantiate() as Entities.Entity;
		entity.Initialize(position, id);
		AddChild(entity);
		return entity;
	}
	
	public Resources.Maps.Layers.EntityLayer ExportTiles()
	{
		Resources.Maps.Layers.EntityLayer layer = new Resources.Maps.Layers.EntityLayer();
		layer.Entities = new Godot.Collections.Dictionary<int, Godot.Collections.Array<Vector2>>();
		foreach (Entities.Entity entity in Entities)
		{
			int id = entity.EntityResource.Id;
			
			if (!layer.Entities.ContainsKey(id))
			{
				layer.Entities.Add(id, new Godot.Collections.Array<Vector2>());
			}

			layer.Entities[id].Add(entity.Position);
		}
		return layer;
	}
	
	public void LoadLayer(Resources.Maps.Layers.EntityLayer entityLayer)
	{
		foreach (var entity in entityLayer.Entities)
		{
			foreach (var position in entity.Value)
			{
				SpawnEntity(position, entity.Key);
			}
		}
	}
	
	public Entities.Entity[] Flash(Vector2 position, int limit)
	{
		var spaceState = GetWorld2D().DirectSpaceState;
		PhysicsPointQueryParameters2D query = new PhysicsPointQueryParameters2D();
		query.Position = GlobalPosition + position;
		query.CollideWithAreas = true;
		var intersectedEntities = spaceState.IntersectPoint(query);
		if (intersectedEntities.Count > 0)
		{
			Entities.Entity[] entities = new Entities.Entity[intersectedEntities.Count];
			for (int i = 0; i < entities.Length; i++)
			{
				Node2D collided = (Node2D)intersectedEntities[i]["collider"];	
				entities[i] = collided as Entities.Entity;
			}
			
			entities = entities.OrderBy(entity => {
				return position.DistanceTo(entity.Position);
			}).ToArray();
			
			return entities;
		} else {
			return null;
		}
	}
	
	public void RemoveEntity(Entities.Entity entity)
	{
		entity.QueueFree();
		RemoveChild(entity);
	}
	
	public Vector2 CellToLocalPos(Vector2I cellPos, bool centered)
	{
		Vector2 center = centered ? new Vector2I(TileSize / 2, TileSize / 2) : Vector2I.Zero;
		return (cellPos * TileSize) + center;
	}
	
	public Vector2 CellToLocalPos(Vector2I cellPos)
	{
		return (cellPos * TileSize);
	}
}
