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
		private Button _toFiringButton;
		
		private AngleControl _angleControl;
		private TraverseControl _traverseControl;
		
		private void UpdateGroundStats(Vector2I position)
		{
			Label groundInfoLabel = _groundInfo.GetNode<Label>("Label");
			groundInfoLabel.Text = $"Lauks: {position}";
		}
		
		private void UpdateEntityStats(Battle.Entities.Device device, Vector2I position)
		{
			//Label primaryInfo = _entityInfo.GetNode<Label>("VBoxContainer/PrimaryInfo");
			//primaryInfo.Text = $"Angle: {device.Angle}deg";
			//seconaryInfo.Text = $"Angle range: {device.DeviceResource.StartingAngle - device.DeviceResource.AngleRadius}deg - {device.DeviceResource.StartingAngle + device.DeviceResource.AngleRadius}deg";
		}
		
		public override void _Ready()
		{
			_entityLayer = GetNode<Node2D>("../../Map/EntityLayer") as Battle.Map.EntityLayer;
			_groundLayer = GetNode<Node2D>("../../Map/GroundLayer") as Battle.Map.GroundLayer;
			_selector = GetNode<Node2D>("../../Map/Selector") as Battle.Map.Selector;
			_entityInfo = GetNode<Control>("EntityInfo");
			_groundInfo = GetNode<Control>("GroundInfo");
			_toFiringButton = GetNode<Button>("ToFiringButton");
			_angleControl = GetNode<HBoxContainer>("EntityInfo/VBoxContainer/AngleControl") as AngleControl;
			_traverseControl = GetNode<HBoxContainer>("EntityInfo/VBoxContainer/TraverseControl") as TraverseControl;
			
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
					_angleControl.SetRange(SelectedEntity.Angle.Min, SelectedEntity.Angle.Max, SelectedEntity.Angle.Value);
					_traverseControl.SetRange(SelectedEntity.Traverse.Locked, SelectedEntity.Traverse.Azimuth);
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
			
			_entityInfo.Connect("mouse_entered", Callable.From(
			() => {
				_selector.Enabled = false;
			}));
			
			_entityInfo.Connect("mouse_exited", Callable.From(
			() => {
				_selector.Enabled = true;
			}));
			
			_angleControl.Connect("Change", Callable.From(
			(float value) => {
				SelectedEntity.Angle.Value = value;
			}));
			
			_traverseControl.Connect("Change", Callable.From(
			(float value) => {
				SelectedEntity.Traverse.Azimuth = value;
			}));
			
			_toFiringButton.Connect("pressed", Callable.From(
			() => {
				(GetParent<Control>() as Battle.Stage.Manager).Firing();
			}));
		}
	}
}
