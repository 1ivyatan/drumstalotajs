using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Stages.PlacementStage
{
	public partial class Placement : Stage
	{
		private Map.Selector selector;
		
		public override void _Ready()
		{
			selector = GetNode<Node2D>("../../../Map/Selector") as Map.Selector;
			selector.Disabled = false;
			selector.Filter = new Map.Selector.SelectorFilter(new int[1], Map.Layers.LayerType.ENTITY);
		}
	}
}
