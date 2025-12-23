using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Entity
{
	TileMapLayer layer;
	Vector2[] instances;
	EntityType id;
	int count;
	
	public void UpdateInstances()
	{
		instances = layer.GetUsedCellsById(0, new Vector2I(0, 0), (int)id)
				 		 .Select(i => new Vector2(i.X, i.Y))
				 		 .ToArray();
		count = instances.Length;
	}
	
	public void SetInstance(Vector2I position)
	{
		layer.SetCell(position, 0, new Vector2I(0, 0), (int)id);
	}
	
	public Entity(TileMapLayer tileMapLayer, EntityType typeId)
	{
		layer = tileMapLayer;
		id = typeId;
		UpdateInstances();
	}
}
