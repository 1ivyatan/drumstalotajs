using Godot;
using System;

namespace Drumstalotajs.Battle.Stage.StageOverlays.DeviceAdjustment
{
	public partial class Stage : Battle.Stage.StageOverlay
	{
		private Battle.Map.Selector _selector;
		private Battle.Map.EntityLayer _entityLayer;
		private Battle.Map.GroundLayer _groundLayer;
		
		private void UpdateGroundStats(Vector2I position)
		{
			GD.Print(position);
		}
		
		public override void _Ready()
		{
			_entityLayer = GetNode<Node2D>("../../Map/EntityLayer") as Battle.Map.EntityLayer;
			_groundLayer = GetNode<Node2D>("../../Map/GroundLayer") as Battle.Map.GroundLayer;
			_selector = GetNode<Node2D>("../../Map/Selector") as Battle.Map.Selector;
			
			_selector.Enabled = true;
			_selector.Layer = Map.Selector.SelectorLayer.All;
			_selector.FilterMode = Map.Selector.SelectorFilterMode.Fitlered;
			_selector.Filter = [ Battle.Entities.Type.Device ];
			
			_entityLayer.RemoveAllEntitiesByType(Entities.Type.DeviceMarker);
			
			_selector.Connect("HoveredGround", new Callable(this, nameof(UpdateGroundStats)));
		}
	}
}
