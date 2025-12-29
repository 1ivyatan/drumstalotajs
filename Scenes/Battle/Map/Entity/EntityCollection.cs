using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class EntityCollection
{
	public long Count
	{
		get;
		private set;
	}
	
	public Dictionary<Vector2I, Entity> Instances
	{
		get;
		private set;
	}
	
	private TileMapLayer parent;
	private Entity.EntityType type;
	
	public EntityCollection(TileMapLayer parent, Entity.EntityType type)
	{
		this.parent = parent;
		this.type = type;
		this.Instances = new Dictionary<Vector2I, Entity>();
		this.Count = 0;
	}
}
