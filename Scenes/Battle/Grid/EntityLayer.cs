using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class EntityLayer : TileMapLayer
{
	public enum EntityType
	{
		DevicePlaceholder = 0,
		Device = 1
	}
	
	Dictionary<int, Vector2[]> entities;
	
	public void UpdateCount(EntityType entityTypeId)
	{
		entities[(int)entityTypeId] = GetUsedCellsById(0, new Vector2I(0, 0), (int)entityTypeId).Select(e => new Vector2(e.X, e.Y)).ToArray();
		GD.Print(entityTypeId + ": " + entities[(int)entityTypeId].Length);
	}
	
	public override void _Ready()
	{
		entities = new Dictionary<int, Vector2[]>();
		
		foreach (EntityType entityTypeId in Enum.GetValues(typeof(EntityType)))
		{
			UpdateCount(entityTypeId);
		}
	}
}
