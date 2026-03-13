using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Mapping.Layers;

public partial class EntityLayer : Node2D
{
	[Export] public Resources.EntitySet EntitySetResource { get; set; }
	public List<Entities.Entity> Entities { get; private set; }
	public int TileSize { get; private set; }
	
	public override void _Ready()
	{
		Entities = new List<Entities.Entity>();
		TileSize = EntitySetResource.TileSize;
		
		ChildEnteredTree += EntitySpawned;
		ChildExitingTree += EntityExited;
	}
	
	private void EntitySpawned(Node node)
	{
		if (node is Entities.Entity)
		{
			Entities.Add(node as Entities.Entity);
		}
	}
	
	private void EntityExited(Node node)
	{
		if (node is Entities.Entity)
		{
			Entities.Remove(node as Entities.Entity);
		}
	}
	
	public Entities.Entity SpawnEntity(Vector2 position, int id)
	{
		Entities.Entity entity = EntitySetResource.Scenes[id].Instantiate() as Entities.Entity;
		entity.Initialize(position);
		AddChild(entity);
		return entity;
	}
	
	public void RemoveEntity(Entities.Entity entity)
	{
		
	}
	
	public Vector2 CellToLocalPos(Vector2I cellPos)
	{
		return (cellPos * TileSize) + new Vector2I(TileSize / 2, TileSize / 2);
	}
}
