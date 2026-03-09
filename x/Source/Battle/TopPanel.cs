using Godot;
using System;

namespace Drumstalotajs.Battle
{
	public partial class TopPanel : PanelContainer
	{
		private Map.Layers.EntityLayer _entityLayer;
		private Button _exitButton;
		private Label _deviceCountLabel;
		private Label _label;
		
		public void SetLabel(string text)
		{
			_label.Text = text;
		}
		
		private void UpdateStats()
		{
			_deviceCountLabel.Text = $"{_entityLayer.Entitys[Entities.Type.Device].Count}";
		}
		
		public override void _Ready()
		{
			_entityLayer = GetNode<TileMapLayer>("../MapContainer/Map/EntityLayer") as Map.Layers.EntityLayer;
			_exitButton = GetNode<Button>("Columns/ExitButton");
			_deviceCountLabel = GetNode<Label>("Columns/Stats/DeviceCount");
			_label = GetNode<Label>("Label");
			
			_entityLayer.Connect(
				"ChangeInEntities", Callable.From((int entityType) => {
					UpdateStats();
				})
			);
		}
	}
}
