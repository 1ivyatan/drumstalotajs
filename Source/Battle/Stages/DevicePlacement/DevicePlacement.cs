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
			_selector.Layer = Map.Selector.SelectorLayer.Entity;
			_selector.FilterMode = Map.Selector.SelectorFilterMode.Fitlered;
			_selector.EntityFilters = [ Battle.Entities.Type.DeviceMarker, Battle.Entities.Type.Device ];
		}
	}
}
