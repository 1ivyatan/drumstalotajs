using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class EntityCollection
{
	TileMapLayer layer;
	Dictionary<Vector2I, Entity> instances;
	EntityType id;
	int count;
	
	public int Count
	{
		get { return count; }
	}
	
	void UpdateCount(int val)
	{
		count = val;
		(layer as EntityLayer).EntityCollectionCountUpdate(id, count);
	}
	
	public bool HasInstanceIn(Vector2I position)
	{
		return instances.ContainsKey(position);
	}
	
	public void CreateInstance(Vector2I position)
	{
		layer.SetCell(position, 0, new Vector2I(0, 0), (int)id);
	}
	
	public void AddInstance(Vector2I position, Entity entity)
	{
		instances.Add(position, entity);
		UpdateCount(count + 1);
	}
	
	public void DetachInstance(Vector2I position)
	{
		if (instances.ContainsKey(position))
		{
			instances.Remove(position);
			UpdateCount(count - 1);
		}
	}
	
	public EntityCollection(TileMapLayer tileMapLayer, EntityType typeId)
	{
		layer = tileMapLayer;
		id = typeId;
		instances = new Dictionary<Vector2I, Entity>();
		count = 0;
	}
}
