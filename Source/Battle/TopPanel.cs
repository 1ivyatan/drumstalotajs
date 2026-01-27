using Godot;
using System;

namespace Drumstalotajs.Battle
{
	public partial class TopPanel : PanelContainer
	{
		private Button _exitButton;
		private Label _deviceCountLabel;
		private Label _label;
		
		public void SetLabel(string text)
		{
			_label.Text = text;
		}
		
		public overrride void _Ready()
		{
			_exitButton = GetNode<Button>("Columns/ExitButton");
			_deviceCountLabel = GetNode<Label>("Columns/Stats/DeviceCount");
			_label = GetNode<Label>("Label");
		}
	}
}
