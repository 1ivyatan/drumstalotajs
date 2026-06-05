using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Components;

public partial class Topnav : Control
{
	[Export] private VFlowContainer _titleContainer;
	[Export] private Control _spacer;
	[Export] private Label _title;
	private int _paddingFromLeft = 0;
	
	public string Title {
		get;
		set {
			field = value;
			_title.Text = field;
		}
	} = "";
	
	public void ToggleTitle(bool toggle)
	{
		_title.Visible = toggle;
	}
	
	public void SetPaddingFromLeft(int shift)
	{
		_paddingFromLeft = shift;
		if (shift == 0)
		{
			_titleContainer.SizeFlagsHorizontal = Control.SizeFlags.ShrinkCenter;
			_spacer.CustomMinimumSize = new Vector2(0, 0);
		} else
		{
			_titleContainer.SizeFlagsHorizontal = Control.SizeFlags.ShrinkBegin;
			_spacer.CustomMinimumSize = new Vector2(shift, 0);
		}
	}
}
