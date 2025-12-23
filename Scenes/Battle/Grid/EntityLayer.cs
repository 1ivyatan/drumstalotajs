using Godot;
using System;
using System.Collections.Generic;

public partial class EntityLayer : TileMapLayer
{
	Dictionary<EntityType, Entity> entities;
	
	public Entity GetEntitiesOfType(EntityType entityTypeId)
	{
		return entities[entityTypeId];
	}
	
	public void PlaceEntity(EntityType entityType, Vector2I position)
	{
		EntityType oldEntityType = (EntityType)GetCellAlternativeTile(position);
		
		if (oldEntityType == EntityType.None)
		{
			
		} else 
		{
			Entity oldEntity = entities[oldEntityType];
			
			if (oldEntity.HasInstanceIn(position))
			{
				oldEntity.DetachInstance(position);
			}
			
			entities[entityType].AddInstance(position);
		}
	}
	
	public override void _Ready()
	{
		entities = new Dictionary<EntityType, Entity>();
		
		foreach (EntityType entityTypeId in Enum.GetValues(typeof(EntityType)))
		{
			if (entityTypeId == EntityType.None)
			{
				continue;
			}
			entities.Add(entityTypeId, new Entity(this, entityTypeId));
		}
	}
}
