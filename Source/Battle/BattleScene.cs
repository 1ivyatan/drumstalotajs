using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Resources.Progress;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Managers.Scenes;
using Drumstalotajs.Managers;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	private Modal _pauseModal;
	private Button _pause;
	private Callable _sceneChangeCall;
	
	public override void _Ready()
	{
		var sceneManager = Nodes.GetRoot().SceneManager;
		Node overlay = GetNode("Overlay");
		_pauseModal = overlay.GetNode("PauseMenu") as Modal;
		_pause = overlay.GetNode<Button>("Topnav/HBoxContainer/Pause");
		_sceneChangeCall = Callable.From<Managers.Scenes.SceneState>(state => {
			switch (state)
			{
				case Managers.Scenes.SceneState.RUNNING:
					_pauseModal.HideModal();
					break;
				case Managers.Scenes.SceneState.PAUSED:
					_pauseModal.ShowModal();
					break;
				default: break;
			}
		});
		
		sceneManager.Connect("StateChanged", _sceneChangeCall);
		
		_pause.Pressed += () =>
		{
			sceneManager.PauseScene();
		};
	}
	
	public override void _ExitTree()
	{
		var sceneManager = Nodes.GetRoot().SceneManager;
		sceneManager.Disconnect("StateChanged", _sceneChangeCall);
	}
	
	public void LoadLevel(LevelSetProps props)
	{
		
	}
		
	
}
/*


	public void PauseScene() { if (State != SceneState.LOADING) State = SceneState.RUNNING; }
	public void ResumeScene() { if (State != SceneState.LOADING) State = SceneState.RUNNING; }
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
}
*/
