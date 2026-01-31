using Godot;
using System;

namespace Drumstalotajs.Battle.Stages
{
	public partial class DevicePlacement : StageOverlay
	{
		private Battle.Map.Selector _selector;
		private Battle.Map.EntityLayer _entityLayer;
		
		private void ClickedOnDeviceTile(int entityType, Vector2I tilePosition)
		{
			_entityLayer.InsertEntity((
				(Entities.Type)entityType == Entities.Type.DeviceMarker
					? Entities.Type.Device
					: Entities.Type.DeviceMarker
			), tilePosition);
		}
		
		public override void _Ready()
		{
			_entityLayer = GetNode<Node2D>("../../Map/EntityLayer") as Battle.Map.EntityLayer;
			_selector = GetNode<Node2D>("../../Map/Selector") as Battle.Map.Selector;

			_selector.Enabled = true;
			_selector.Layer = Map.Selector.SelectorLayer.Entity;
			_selector.FilterMode = Map.Selector.SelectorFilterMode.Fitlered;
			_selector.Filter = [ Battle.Entities.Type.DeviceMarker, Battle.Entities.Type.Device ];
			
			_selector.Connect("SelectedEntity", new Callable(this, nameof(ClickedOnDeviceTile)));
		}
	}
}
