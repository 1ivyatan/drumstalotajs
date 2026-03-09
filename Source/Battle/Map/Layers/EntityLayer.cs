using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Drumstalotajs.Battle.Map.Layers
{
	public partial class EntityLayer : Layer
	{
		[Signal] public delegate void ChangeInEntitiesEventHandler(int entityType);
		[Signal] public delegate void AddedEntityEventHandler(Entities.Entity entity, Vector2I position);
		[Signal] public delegate void RemovedEntityEventHandler(Entities.Entity entity, Vector2I position);
		
		public Dictionary<Entities.Type, List<Entities.Entity>> Entitys { get; private set; }
		
		public Entities.Entity GetEntity(Vector2I position)
		{
			foreach (var entityType in Entitys)
			{
				
				//if (GetCellPos(entity.Position) == position) return entity;
			}
			return null;
		}
		
		public Entities.Entity GetEntity(Entities.Type entityType, Vector2I position)
		{
			foreach (var entity in Entitys[entityType])
			{
				GD.Print(entity.Position);
				GD.Print(GetCellPos(entity.Position));
				if (GetCellPos(entity.Position) == position) return entity;
			}
			return null;
		}
		
		public void InsertEntity(Entities.Id entityId, Vector2I position)
		{
			SetCell(position, 0, new Vector2I(0, 0), (int)entityId);
		}
		
		public void RemoveEntity(Vector2I position)
		{
			EraseCell(position);
		}
		
		public void RemoveEntity(Entities.Entity entity)
		{
			EraseCell(GetCellPos(entity.Position));
		}
		
		public void RemoveAllEntitiesByType(Entities.Type type)
		{
			foreach (var cell in Entitys[type])
			{
				RemoveEntity(cell);
			}
		}
		
		public override void _Ready()
		{
			Entitys = new Dictionary<Entities.Type, List<Entities.Entity>>();
			
			foreach (Entities.Type type in Enum.GetValues(typeof(Entities.Type)))
			{
				if (type == Entities.Type.None)
				{
					continue;
				}
				Entitys.Add(type, new List<Entities.Entity>());
			}
			
			ChildEnteredTree += _EntityEntered;
			ChildExitingTree += _EntityLeft;
		}

		private void _EntityEntered(Node node)
		{
			if (node is Entities.Entity)
			{
				Entities.Entity entity = node as Entities.Entity;
				Vector2 localPos = ToLocal(entity.Position);
				Vector2I cellPos = LocalToMap(localPos);
				
				if ((Entities.Type)entity.EntityResource.Type != Entities.Type.None)
				{
					Entitys[(Entities.Type)entity.EntityResource.Type].Add(entity);
					EmitSignal("AddedEntity", entity, cellPos);
				//	EmitSignal("ChangeInEntities", entity.EntityResource.Type);
				}
			}
		}

		private void _EntityLeft(Node node)
		{
			if (node is Entities.Entity)
			{
				Entities.Entity entity = node as Entities.Entity;
				
				Vector2 localPos = ToLocal(entity.Position);
				Vector2I cellPos = LocalToMap(localPos);
				
				if ((Entities.Type)entity.EntityResource.Type != Entities.Type.None)
				{
					Entitys[(Entities.Type)entity.EntityResource.Type].Remove(entity);
					EmitSignal("RemovedEntity", entity, cellPos);
				//	EmitSignal("ChangeInEntities", entity.EntityResource.Type);
				}
			}
		}
		
		public Entities.Type GetEntityType(Vector2I position)
		{
			return (Entities.Type)GetCellAlternativeTile(position);
		}
	}
}
