using Godot;
using System;
using System.Collections.Generic;

namespace Drumstalotajs.Battle.Map
{
	public partial class EntityLayer : TileMapLayer
	{
		[Signal]
		public delegate void AddedEntityEventHandler(Entities.Entity entity);
		
		[Signal]
		public delegate void RemovedEntityEventHandler(Entities.Entity entity);
		
		public Dictionary<Entities.Type, Dictionary<Vector2I, Entities.Entity>> EntityPointers
		{
			get;
			private set;
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
			
			ChildEnteredTree += EntityEntered;
			ChildExitingTree += EntityLeft;
		}

		private void EntityEntered(Node node)
		{
			if (node is Entities.Entity)
			{
				Entities.Entity entity = node as Entities.Entity;
				Vector2 localPos = ToLocal(entity.Position);
				Vector2I cellPos = LocalToMap(localPos);
				
				if ((Entities.Type)entity.EntityResource.Type != Entities.Type.None)
				{
					EntityPointers[(Entities.Type)entity.EntityResource.Type].Add(cellPos, entity);
					EmitSignal(entity);
				}
			}
		}

		private void EntityLeft(Node node)
		{
			if (node is Entities.Entity)
			{
				Entities.Entity entity = node as Entities.Entity;
				Vector2 localPos = ToLocal(entity.Position);
				Vector2I cellPos = LocalToMap(localPos);
				
				if ((Entities.Type)entity.EntityResource.Type != Entities.Type.None)
				{
					EntityPointers[(Entities.Type)entity.EntityResource.Type].Remove(cellPos);
					EmitSignal(entity);
				}
			}
		}
	}
}
