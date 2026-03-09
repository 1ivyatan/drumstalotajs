using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Drumstalotajs.Scenes.BattleScene.Map.Layers
{
	public partial class EntityLayer : Layer
	{
		public Dictionary<Entities.Type, List<Entities.Entity>> Entities { get; private set; }
		
		public override void _Ready()
		{
			ChildEnteredTree += _EntityEntered;
			ChildExitingTree += _EntityLeft;
		}
	}
}
