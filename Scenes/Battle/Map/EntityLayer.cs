using Godot;
using System;
using System.Collections.Generic;

public partial class EntityLayer : TileMapLayer
{
	private Dictionary<Entity.EntityType, EntityCollection> collections;
	
	public override void _Ready()
	{
		this.collections = new Dictionary<Entity.EntityType, EntityCollection>();
		
		foreach (Entity.EntityType entityType in Enum.GetValues(typeof(Entity.EntityType)))
		{
			if (entityType == Entity.EntityType.None)
			{
				continue;
			}
			
			this.collections.Add(entityType, new EntityCollection(this, entityType));
		}
	}
}
