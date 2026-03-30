using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Components.Modals;

namespace Drumstalotajs.Start;

public partial class StartScene : Control
{
	private Modal _modal;
	private Button _startButton;
	private Button _annotationButton;
	private Button _quitButton;

	public override void _Ready()
	{
		Node buttons = GetNode("Grid/MenuColumn/Buttons");
		_modal = GetNode("AnnotationModal") as Modal;
		_startButton = buttons.GetNode<Button>("Start");
		_annotationButton = buttons.GetNode<Button>("Annotation");
		_quitButton = buttons.GetNode<Button>("Quit");
		_annotationButton.Pressed += () => {
			_modal.Show();
		};
	}
}
