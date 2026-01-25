using Godot;
using System;
using System.Collections.Generic;

public partial class EntityLayer : TileMapLayer
{
	[Signal]
	public delegate void EntitySpawnedEventHandler(int entityType, Node2D newEntity);
	
	[Signal]
	public delegate void EntityDestroyedEventHandler(int entityType, Node2D oldEntity);
	
	public Dictionary<Entity.EntityType, EntityCollection> EntityCollections
	{
		get;
		private set;
	}
	
	public void InsertEntity(Entity.EntityType entityType, Vector2I position)
	{
		this.SetCell(position, 0, new Vector2I(0, 0), (int)entityType);
	}
	
	public void EraseEntity(Vector2I position)
	{
		this.EraseCell(position);
	}
	
	public void EraseEntitiesByType(Entity.EntityType entityType)
	{
		foreach(KeyValuePair<Vector2I, Entity> entity in this.EntityCollections[(Entity.EntityType)entityType].Instances)
		{
			this.EraseEntity(entity.Key);
		}
	}
	
	public override void _Ready()
	{
		this.EntityCollections = new Dictionary<Entity.EntityType, EntityCollection>();
		
		foreach (Entity.EntityType entityType in Enum.GetValues(typeof(Entity.EntityType)))
		{
			if (entityType == Entity.EntityType.None)
			{
				continue;
			}
			
			this.EntityCollections.Add(entityType, new EntityCollection(this, entityType));
		}
	}
	
	public void _EntitySpawned(int entityType, Entity entity)
	{
		this.EntityCollections[(Entity.EntityType)entityType].Add(this.LocalToMap(entity.Position), entity);
		this.EmitSignal(SignalName.EntitySpawned, entityType, entity);
	}
	
	public void _EntityDestroyed(int entityType, Entity entity)
	{
		this.EntityCollections[(Entity.EntityType)entityType].Remove(this.LocalToMap(entity.Position));
		this.EmitSignal(SignalName.EntityDestroyed, entityType, entity);
	}
}
