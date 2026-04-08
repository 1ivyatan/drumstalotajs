using Godot;
using System;

namespace Drumstalotajs.Editor;

public partial class ModeContainer : Node
{
	private Label _label;
	
	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
	}
	
	public void SetModeText(EditMode mode)
	{
		switch (mode)
		{
			case EditMode.Idle: _label.Text = $"Mode: Idle"; break;
			case EditMode.Insert: _label.Text = $"Mode: Insert"; break;
			case EditMode.Edit: _label.Text = $"Mode: Edit"; break;
			default: break;
		}
	}
}
