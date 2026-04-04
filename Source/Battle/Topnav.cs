using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Battle;

public partial class Topnav : Node
{
	private Button _pause;
	private Label _title;
	
	public override void _Ready()
	{
		var sceneManager = Nodes.GetRoot().SceneManager;
		_title = GetNode<Label>("Title");
		_pause = GetNode<Button>("Pause");
		_pause.Pressed += () =>
		{
			sceneManager.PauseScene();
		};
	}
	
	public void SetTitle(string text)
	{
		_title.Text = text;
	}
}
