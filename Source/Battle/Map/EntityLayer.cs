using Godot;
using System;
using System.Collections.Generic;

namespace Drumstalotajs.Battle.Map
{
	public partial class EntityLayer : TileMapLayer
	{
		[Signal]
		public delegate void ChangeInEntitiesEventHandler(int entityType);
		
		[Signal]
		public delegate void AddedEntityEventHandler(Entities.Entity entity, Vector2I position);
		
		[Signal]
		public delegate void RemovedEntityEventHandler(Entities.Entity entity, Vector2I position);
		
		public Dictionary<Entities.Type, Dictionary<Vector2I, Entities.Entity>> EntityPointers
		{
			get;
			private set;
		}
		
		public void InsertEntity(Entities.Id entityId, Vector2I position)
		{
			SetCell(position, 0, new Vector2I(0, 0), (int)entityId);
		}
		
		public void RemoveEntity(Vector2I position)
		{
			
		}
		
		public void RemoveAllEntitiesByType(Entities.Type type)
		{
			
		}
		
		public override void _Ready()
		{
			EntityPointers = new Dictionary<Entities.Type, Dictionary<Vector2I, Entities.Entity>>();
			
			foreach (Entities.Type type in Enum.GetValues(typeof(Entities.Type)))
			{
				if (type == Entities.Type.None)
				{
					continue;
				}
				
				EntityPointers.Add(type, new Dictionary<Vector2I, Entities.Entity>());
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
					EntityPointers[(Entities.Type)entity.EntityResource.Type].Add(cellPos, entity);
					EmitSignal("AddedEntity", entity, cellPos);
					EmitSignal("ChangeInEntities", entity.EntityResource.Type);
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
					EntityPointers[(Entities.Type)entity.EntityResource.Type].Remove(cellPos);
					EmitSignal("RemovedEntity", entity, cellPos);
					EmitSignal("ChangeInEntities", entity.EntityResource.Type);
				}
			}
		}
	}
}
