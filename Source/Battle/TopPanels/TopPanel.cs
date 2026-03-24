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
		pauseButton = GetNode<Button>("PauseButton");
		title = GetNode<Label>("Title");
		counters = GetNode<Container>("Counters");
	}
	
	public void SetTitle(string text)
	{
		title.Text = text;
	}
}
