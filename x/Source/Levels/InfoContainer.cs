using Godot;
using System;

namespace Drumstalotajs.Levels
{
	public partial class InfoContainer : VBoxContainer
	{
		private TextEdit _desc;
		
		public void SetInfo(Resources.Levels.LevelProps levelProps)
		{
			_desc.Text = $"{levelProps.Level.Title}\nElevation: {levelProps.Level.BaseHeight}m\n{levelProps.Level.Description}";
		}
		
		public void SetInfo()
		{
			_desc.Text = "Select a level in the map for information!";
		}
		
		public override void _Ready()
		{
			_desc = GetNode<TextEdit>("Desc");
			SetInfo();
		}
	}
}
