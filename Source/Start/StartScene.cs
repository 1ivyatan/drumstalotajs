using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Components.Modals;

namespace Drumstalotajs.Start;

public partial class StartScene : Control
{
	private Modal _modal;
	private Button _startButton;
	private Button _aboutButton;
	private Button _quitButton;

	public override void _Ready()
	{
		Node buttons = GetNode("Grid/MenuColumn/Buttons");
		_modal = GetNode("AnnotationModal") as Modal;
		_startButton = buttons.GetNode<Button>("Start");
		_aboutButton = buttons.GetNode<Button>("About");
		_quitButton = buttons.GetNode<Button>("Quit");
		_aboutButton.Pressed += () => {
			_modal.Show();
		};
	}
}
