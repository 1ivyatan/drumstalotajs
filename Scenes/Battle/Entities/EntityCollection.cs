using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class EntityCollection
{
	TileMapLayer layer;
	List<Vector2I> instances;
	EntityType id;
	int count;
	
	public int Count
	{
		get { return count; }
	}
	
	public void UpdateInstances()
	{
		instances = layer.GetUsedCellsById(0, new Vector2I(0, 0), (int)id).ToList();
		count = instances.Count;
	}
	
	public bool HasInstanceIn(Vector2I position)
	{
		return instances.Contains(position);
	}
	
	public void AddInstance(Vector2I position)
	{
		layer.SetCell(position, 0, new Vector2I(0, 0), (int)id);
		instances.Add(position);
		count++;
	}
	
	public void DetachInstance(Vector2I position)
	{
		if (instances.Contains(position))
		{
			instances.Remove(position);
			count--;
		}
	}
	
	public void RemoveInstance(Vector2I position)
	{
		if (instances.Contains(position))
		{
			layer.EraseCell(position);
			instances.Remove(position);
			count--;
		}
	}
	
	public EntityCollection(TileMapLayer tileMapLayer, EntityType typeId)
	{
		layer = tileMapLayer;
		id = typeId;
		UpdateInstances();
	}
}
