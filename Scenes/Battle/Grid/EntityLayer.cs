using Godot;
using System;
using System.Collections.Generic;

public partial class EntityLayer : TileMapLayer
{
	[Signal]
	public delegate void EntityCountUpdatedEventHandler(int entityTypeId, int count);
	
	Dictionary<EntityType, EntityCollection> entityCollections;
	
	public void OnEntitySpawn(Node2D entity, int entityType)
	{
		entityCollections[(EntityType)entityType].AddInstance(LocalToMap(entity.Position), entity as Entity);
	}
	
	public EntityCollection GetEntitiesOfType(EntityType entityTypeId)
	{
		return entityCollections[entityTypeId];
	}
	
	public void InsertEntity(EntityType entityType, Vector2I position)
	{
		EntityType oldEntityType = (EntityType)GetCellAlternativeTile(position);
		EntityCollection oldEntity = entityCollections[oldEntityType];
		
		if (oldEntity.HasInstanceIn(position))
		{
			oldEntity.DetachInstance(position);
		}
		
		entityCollections[entityType].CreateInstance(position);
		GD.Print(entityType + " " + position);
		/* rewrite!!!! */
		/*
		EntityType oldEntityType = (EntityType)GetCellAlternativeTile(position);
		
		if (oldEntityType == EntityType.None)
		{
			EraseCell(position);
		} else 
		{
			EntityCollection oldEntity = entities[oldEntityType];
			
			if (oldEntity.HasInstanceIn(position))
			{
				oldEntity.DetachInstance(position);
			}
			
//			entities[entityType].AddInstance(position);
		}*/
	}
	
	public override void _Ready()
	{
		entityCollections = new Dictionary<EntityType, EntityCollection>();
		
		foreach (EntityType entityTypeId in Enum.GetValues(typeof(EntityType)))
		{
			if (entityTypeId == EntityType.None)
			{
				continue;
			}
			
			entityCollections.Add(entityTypeId, new EntityCollection(this, entityTypeId));
		}
	}
	
	public void EntityCollectionCountUpdate(EntityType entityType, int count)
	{
		EmitSignal(SignalName.EntityCountUpdated, (int)entityType, count);
	}
}
