using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Start;

public partial class StartScene : Control
{
	[Export] private Button _startButton;
	[Export] private Button _aboutButton;
	[Export] private Button _editorButton;
	[Export] private Button _quitButton;

	public override void _Ready()
	{
		_aboutButton.Pressed += () => {
		//	_modal.Show();
		};
		_editorButton.Pressed += () => {
			Nodes.GetRoot().SceneManager.Editor();
		};
	}
}
