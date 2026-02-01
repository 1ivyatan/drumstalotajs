using Godot;
using System;

namespace Drumstalotajs.Battle.Stage.StageOverlays.DeviceAdjustment
{
	public partial class Stage : Battle.Stage.StageOverlay
	{
		private Battle.Map.Selector _selector;
		private Battle.Map.EntityLayer _entityLayer;
		private Battle.Map.GroundLayer _groundLayer;
		private Control _groundInfo;
		private Label _groundInfoLabel;
		
		private void ToggleGroundStats(bool toggle)
		{
			_groundInfo.Visible = toggle;
		}
		
		private void UpdateGroundStats(Vector2I position)
		{
			_groundInfoLabel.Text = $"Lauks: {position}";
		}
		
		public override void _Ready()
		{
			_entityLayer = GetNode<Node2D>("../../Map/EntityLayer") as Battle.Map.EntityLayer;
			_groundLayer = GetNode<Node2D>("../../Map/GroundLayer") as Battle.Map.GroundLayer;
			_selector = GetNode<Node2D>("../../Map/Selector") as Battle.Map.Selector;
			_groundInfo = GetNode<Control>("GroundInfo");
			_groundInfoLabel = GetNode<Label>("GroundInfo/Label");
			
			_selector.Enabled = true;
			_selector.Layer = Map.Selector.SelectorLayer.All;
			_selector.FilterMode = Map.Selector.SelectorFilterMode.Fitlered;
			_selector.Filter = [ Battle.Entities.Type.Device ];
			
			_entityLayer.RemoveAllEntitiesByType(Entities.Type.DeviceMarker);
			
			_selector.Connect("HoveredGround", Callable.From(
			(Vector2I position) => {
				UpdateGroundStats(position);
				ToggleGroundStats(true);
			}));
			
			_selector.Connect("HoveredEmptyGround", Callable.From(
			(Vector2I position) => {
				ToggleGroundStats(false);
			}));
		}
	}
}
