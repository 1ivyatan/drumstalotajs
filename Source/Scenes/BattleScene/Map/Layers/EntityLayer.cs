using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Drumstalotajs.Scenes.BattleScene.Map.Layers
{
	public partial class EntityLayer : Layer
	{
		public Dictionary<BattleScene.Map.Entities.Type, List<Entities.Entity>> Entities { get; private set; }
		
		public override void _Ready()
		{
			Entities = new Dictionary<BattleScene.Map.Entities.Type, List<Entities.Entity>>();
			
			foreach (BattleScene.Map.Entities.Type type in Enum.GetValues(typeof(BattleScene.Map.Entities.Type)))
			{
				if (type == BattleScene.Map.Entities.Type.NONE) { continue; }
				Entities.Add(type, new List<Entities.Entity>());
			}
			
			ChildEnteredTree += EntityEntered;
			ChildExitingTree += EntityExited;
		}

		private void EntityEntered(Node node)
		{
			if (node is Entities.Entity)
			{
				Entities.Entity entity = node as Entities.Entity;
				BattleScene.Map.Entities.Type entityType = entity.EntityResource.GetEntityType();
				if (entityType != BattleScene.Map.Entities.Type.NONE)
				{
					Entities[entityType].Add(entity);
				}
			}
		}
		
		private void EntityExited(Node node)
		{
			if (node is Entities.Entity)
			{
				Entities.Entity entity = node as Entities.Entity;
				Entities.Type entityType = entity.EntityResource.GetEntityType();
				if (entityType != BattleScene.Map.Entities.Type.NONE)
				{
					Entities[entityType].Remove(entity);
				}
			}
		}
	}
}
