using Godot;
using System;

namespace drumstalotajs.Battle.TopPanels;

public partial class TopPanel : Control
{
	private Button pauseButton;
	private Label title;
	private Container counters;
	
	public override void _Ready()
	{
		BattleScene battleScene = GetNode<Node2D>("../..") as BattleScene;
		pauseButton = GetNode<Button>("PauseButton");
		title = GetNode<Label>("Title");
		//counters = GetNode<Container>("Counters");
		
		pauseButton.Pressed += () => {
			battleScene.Pause();
		};
	}
	
	public void SetTitle(string text)
	{
		title.Text = text;
	}
}
