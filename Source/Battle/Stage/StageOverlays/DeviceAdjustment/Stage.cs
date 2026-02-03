using Godot;
using System;

namespace Drumstalotajs.Battle.Stage.StageOverlays.DeviceAdjustment
{
	public partial class Stage : Battle.Stage.StageOverlay
	{
		private Entities.Device SelectedEntity { get; set; }
		
		private Battle.Map.Selector _selector;
		private Battle.Map.EntityLayer _entityLayer;
		private Battle.Map.GroundLayer _groundLayer;
		private Control _groundInfo;
		private Control _entityInfo;
		
		private void UpdateGroundStats(Vector2I position)
		{
			Label groundInfoLabel = _groundInfo.GetNode<Label>("Label");
			groundInfoLabel.Text = $"Lauks: {position}";
		}
		
		private void UpdateEntityStats(Battle.Entities.Device device, Vector2I position)
		{
			Label entityInfoLabel = _entityInfo.GetNode<Label>("Label");
			entityInfoLabel.Text = $"{device.Angle}";
		}
		
		public override void _Ready()
		{
			_entityLayer = GetNode<Node2D>("../../Map/EntityLayer") as Battle.Map.EntityLayer;
			_groundLayer = GetNode<Node2D>("../../Map/GroundLayer") as Battle.Map.GroundLayer;
			_selector = GetNode<Node2D>("../../Map/Selector") as Battle.Map.Selector;
			_groundInfo = GetNode<Control>("GroundInfo");
			_entityInfo = GetNode<Control>("EntityInfo");
			
			_selector.Enabled = true;
			_selector.Layer = Map.Selector.SelectorLayer.All;
			_selector.FilterMode = Map.Selector.SelectorFilterMode.Fitlered;
			_selector.Filter = [ Battle.Entities.Type.Device ];
			
			_entityLayer.RemoveAllEntitiesByType(Entities.Type.DeviceMarker);

			_selector.Connect("SelectedEntity", Callable.From(
			(int entityType, Vector2I position) => {
				if ((Entities.Type)entityType == Entities.Type.Device)
				{
					SelectedEntity = _entityLayer.EntityPointers[(Entities.Type)entityType][position] as Entities.Device;
					UpdateEntityStats(SelectedEntity, position);
					_entityInfo.Visible = true;
				}
			}));
				
			_selector.Connect("SelectedEmptyEntity", Callable.From(
			(Vector2I position) => {
				_entityInfo.Visible = false;
			}));
			
			_selector.Connect("HoveredGround", Callable.From(
			(Vector2I position) => {
				UpdateGroundStats(position);
				_groundInfo.Visible = true;
			}));
			
			_selector.Connect("HoveredEmptyGround", Callable.From(
			(Vector2I position) => {
				_groundInfo.Visible = false;
			}));
		}
	}
}
