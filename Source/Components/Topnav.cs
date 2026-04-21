using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Components;

public partial class Topnav : Control
{
	[Export] private Label _title;
	public string Title {
		get;
		set {
			field = value;
			_title.Text = field;
		}
	} = "";
}
