using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;
using Drumstalotajs.Components.Modals;

namespace Drumstalotajs.Start;

public partial class StartScene : Control
{
	private Modal _modal;
	[Export] private Button _startButton;
	[Export] private Button _aboutButton;
	[Export] private Button _editorButton;
	[Export] private Button _quitButton;

	public override void _Ready()
	{
		Node buttons = GetNode("Grid/MenuColumn/Buttons");
		_modal = GetNode("AnnotationModal") as Modal;
		if (!OS.HasFeature("editor"))
		{
			_editorButton.Visible = false;
			_editorButton.Disabled = true;
		}
		_aboutButton.Pressed += () => {
			_modal.Show();
		};
		_editorButton.Pressed += () => {
			if (OS.HasFeature("editor"))
			{
				Nodes.GetRoot().SceneManager.Editor();
			}
		};
	}
}
