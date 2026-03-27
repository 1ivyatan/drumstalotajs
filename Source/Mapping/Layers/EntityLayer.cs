using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

namespace drumstalotajs.Mapping.Layers;

public partial class EntityLayer : Node2D
{
	[Signal] public delegate void EntityEnteredEventHandler(Entities.Entity entity);
	[Signal] public delegate void EntityExitingEventHandler(Entities.Entity entity);
	
	[Export] public Resources.Sets.Entities.EntitySet EntitySetResource { get; set; }
	
	public OrderedDictionary<int, PackedScene> EntityScenes { get; private set; }
	public List<Entities.Entity> Entities { get; private set; }
	public int TileSize { get; private set; }
	
	public override void _Ready()
	{
		EntityScenes = new OrderedDictionary<int, PackedScene>();
		Entities = new List<Entities.Entity>();
		TileSize = EntitySetResource.TileSize;
		
		foreach (var props in EntitySetResource.Entities)
		{
			EntityScenes.Add(props.Id, props.Scene);
		}
		
		ChildEnteredTree += EntitySpawned;
		ChildExitingTree += EntityExited;
	}
	
	private void EntitySpawned(Node node)
	{
		if (node is Entities.Entity)
		{
			Entities.Entity entity = node as Entities.Entity;
			Entities.Add(entity);
			EmitSignal(SignalName.EntityEntered, entity);
		}
	}
	
	private void EntityExited(Node node)
	{
		if (node is Entities.Entity)
		{
			Entities.Entity entity = node as Entities.Entity;
			EmitSignal(SignalName.EntityExiting, entity);
			Entities.Remove(entity);
		}
	}
	
	public Entities.Entity SpawnEntity(Vector2 position, int id)
	{
		Entities.Entity entity = EntityScenes[id].Instantiate() as Entities.Entity;
		entity.Initialize(position, id);
		AddChild(entity);
		return entity;
	}
	
	public Entities.Entity SpawnEntity(Vector2 position, double azimuth, int id)
	{
		Entities.Entity entity = EntityScenes[id].Instantiate() as Entities.Entity;
		entity.Initialize(position, azimuth, id);
		AddChild(entity);
		return entity;
	}
	
	public Resources.Maps.Layers.EntityLayer ExportTiles()
	{
		Resources.Maps.Layers.EntityLayer layer = new Resources.Maps.Layers.EntityLayer();
		layer.Entities = new Godot.Collections.Dictionary<int, Godot.Collections.Array<Resources.Maps.Layers.Entities.EntityTransform>>();
		foreach (Entities.Entity entity in Entities)
		{
			int id = entity.EntityResource.Id;
			
			if (!layer.Entities.ContainsKey(id))
			{
				layer.Entities.Add(id, new Godot.Collections.Array<Resources.Maps.Layers.Entities.EntityTransform>());
			}

			Resources.Maps.Layers.Entities.EntityTransform entityProperties = new Resources.Maps.Layers.Entities.EntityTransform();
			entityProperties.Position = entity.Position;
			entityProperties.Azimuth = entity.Azimuth;
			layer.Entities[id].Add(entityProperties);
		}
		return layer;
	}
	
	public void LoadLayer(Resources.Maps.Layers.EntityLayer entityLayer)
	{
		foreach (var entity in entityLayer.Entities)
		{
			foreach (var properties in entity.Value)
			{
				SpawnEntity(properties.Position, properties.Azimuth, entity.Key);
			}
		}
	}
	
	public Entities.Entity[] GetEntitiesById(int id)
	{
		return Entities.Where(e => e.EntityResource.Id == id).ToArray();
	}
	
	public Entities.Entity[] GetEntitiesByType(Entities.EntityType entityType)
	{
		return Entities.Where(e => e.EntityResource.EntityType == entityType).ToArray();
	}
	
	public Entities.Entity[] FlashEntities(Vector2 localPos, int limit)
	{
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
		
		return [];
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
	
	public Entities.Entity[] GetEntities(int id)
	{
		return Entities.Where(entity => entity.EntityResource.Id == id).ToArray();
	}
}
