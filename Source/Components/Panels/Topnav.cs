using Godot;
using System;

namespace Drumstalotajs.Components.Panels;

public partial class Topnav : Control
{
	[Export] private Label _title;
	
	public void SetTitle(string text)
	{
		_title.Text = text;
	}
}
