using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Battle;

public partial class Pause : Components.Modals.Window
{
	private Button _resume;
	private Button _retreat;
	
	public override void _Ready()
	{
		var sceneManager = Nodes.GetRoot().SceneManager;
		_resume = GetNode<Button>("Resume");
		_retreat = GetNode<Button>("ToLevelSelection");
		
		_resume.Pressed += () =>
		{
			sceneManager.ResumeScene();
		};
		
		_retreat.Pressed += () =>
		{
			sceneManager.LevelSelection();
		};
	}
}
