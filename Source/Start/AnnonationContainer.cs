using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Managers.Audio;

namespace Drumstalotajs.Start;

public partial class AnnonationContainer : Control
{
	[Export] private RichTextLabel _text;
	[Export] private Button _close;

	public override void _Ready()
	{
		_close.Pressed += () => {
			Visible = false;
		};
	}
	
	public void SetText(string text)
	{
		_text.Text = text;
	}
}
