using Godot;
using System;
using System.Collections.Generic;

public partial class EntityLayer : TileMapLayer
{
	Dictionary<int, Entity> entities;
	
	public Entity GetEntity(EntityType entityTypeId)
	{
		return entities[(int)entityTypeId];
	}
	
	public override void _Ready()
	{
		entities = new Dictionary<int, Entity>();
		
		foreach (EntityType entityTypeId in Enum.GetValues(typeof(EntityType)))
		{
			entities.Add((int)entityTypeId, new Entity(this, entityTypeId));
		}
	}
}
