using Godot;
using System;

namespace Drumstalotajs.Battle.Stages
{
	public partial class DevicePlacement : StageOverlay
	{
		private Battle.Map.Selector _selector;
		
		public override void _Ready()
		{
			_selector = GetNode<Node2D>("../../Map/Selector") as Battle.Map.Selector;

			_selector.Enabled = true;
		}
	}
}
