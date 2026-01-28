using Godot;
using System;
using System.Collections.Generic;

namespace Drumstalotajs.Battle.Map
{
	public partial class EntityLayer : TileMapLayer
	{
		public Dictionary<Entities.Type, Dictionary<Vector2I, Entities.Entity>> Entities
		{
			get;
			private set;
		}
		
		public override void _Ready()
		{
			ChildEnteredTree += EntityEntered;
			ChildExitingTree += EntityLeft;
		}

		private void EntityEntered(Node node)
		{
			GD.Print(node);
		}

		private void EntityLeft(Node node)
		{
			GD.Print(node);
		}
	}
}
