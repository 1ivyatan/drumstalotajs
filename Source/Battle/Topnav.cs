using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Battle;

public partial class Topnav : Node
{
	private Button _pause;
	
	public override void _Ready()
	{
		var sceneManager = Nodes.GetRoot().SceneManager;
		_pause = GetNode<Button>("Pause");
		_pause.Pressed += () =>
		{
			sceneManager.PauseScene();
		};
	}
}
