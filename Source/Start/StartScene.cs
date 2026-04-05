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
	private Button _annotationButton;
	private Button _quitButton;

	public override void _Ready()
	{
		Node buttons = GetNode("Grid/MenuColumn/Buttons");
		_modal = GetNode("AnnotationModal") as Modal;
		_startButton = buttons.GetNode<Button>("Start");
		_annotationButton = buttons.GetNode<Button>("Annotation");
		_quitButton = buttons.GetNode<Button>("Quit");
		_startButton.Pressed += () => {
			Nodes.GetRoot().SceneManager.LevelSelection();
		};
		_annotationButton.Pressed += () => {
			_modal.Show();
		};
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("open_editor"))
		{
			Nodes.GetRoot().SceneManager.Editor();
		}
	}
}
