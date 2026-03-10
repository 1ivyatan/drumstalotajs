using Godot;
using System;

namespace drumstalotajs.Start;

public partial class StartScene : CanvasLayer
{
	private Managers.SceneManager sceneManager;
	private Button toStartButton;

	public override void _Ready()
	{
		sceneManager = GetNode<Node>("../") as Managers.SceneManager;
		toStartButton = GetNode<Button>("ToStartButton");
		toStartButton.Pressed += () => {
			sceneManager.LevelsScene();
		};
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.Pressed)
		{
			if (keyEvent.Keycode == Key.E && keyEvent.CtrlPressed)
			{
				sceneManager.EditorScene();
			}
		}
	}
}
