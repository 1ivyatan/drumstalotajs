using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;
using Drumstalotajs.Components.Modals;

namespace Drumstalotajs.Start;

public partial class StartScene : Control
{
	private Modal _modal;
	private Button _startButton;
	private Button _aboutButton;
	private Button _editorButton;
	private Button _quitButton;

	public override void _Ready()
	{
		Node buttons = GetNode("Grid/MenuColumn/Buttons");
		_modal = GetNode("AnnotationModal") as Modal;
		_startButton = buttons.GetNode<Button>("Start");
		_editorButton = buttons.GetNode<Button>("Editor");
		_aboutButton = buttons.GetNode<Button>("About");
		_quitButton = buttons.GetNode<Button>("Quit");
		_aboutButton.Pressed += () => {
			_modal.Show();
		};
		_editorButton.Pressed += () => {
			Nodes.GetRoot().SceneManager.Editor();
		};
	}
}
