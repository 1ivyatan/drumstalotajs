using Godot;
using System;

namespace Drumstalotajs.Battle.Stages
{
	public partial class DeviceAdjustment : StageOverlay
	{
		private Battle.Map.Selector _selector;
		private Battle.Map.EntityLayer _entityLayer;
		
		public override void _Ready()
		{
			_entityLayer = GetNode<Node2D>("../../Map/EntityLayer") as Battle.Map.EntityLayer;
			_selector = GetNode<Node2D>("../../Map/Selector") as Battle.Map.Selector;
			
			if (!(_entityLayer.EntityPointers[Entities.Type.Device].Count > 0))
			{
				(GetParent<Control>() as Stages.Manager).DeviceAdjustment();
				return;
			}
		}
	}
}
