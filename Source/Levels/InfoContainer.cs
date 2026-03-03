using Godot;
using System;

namespace Drumstalotajs.Levels
{
	public partial class InfoContainer : VBoxContainer
	{
		private TextEdit _desc;
		private Button _toBattleButton;
		
		public void SetInfo()
		{
			_desc.Text = "Select a level in the map for information!";
			_toBattleButton.Disabled = true;
		}
		
		public override void _Ready()
		{
			_desc = GetNode<TextEdit>("Desc");
			_toBattleButton = GetNode<Button>("ToBattleButton");
			SetInfo();
		}
	}
}
