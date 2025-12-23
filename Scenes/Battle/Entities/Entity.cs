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
	
	public void UpdateInstances()
	{
		instances = layer.GetUsedCellsById(0, new Vector2I(0, 0), (int)id)
				 		 .Select(i => new Vector2(i.X, i.Y))
				 		 .ToArray();
	}
	
	public Entity(TileMapLayer tileMapLayer, EntityType typeId)
	{
		layer = tileMapLayer;
		id = typeId;
		UpdateInstances();
	}
}
