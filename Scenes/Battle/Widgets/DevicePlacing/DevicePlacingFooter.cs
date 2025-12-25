using Godot;
using System;

public partial class DevicePlacingFooter : Widget
{
	protected override void LoadWidget()
	{
		Button exitButton = GetNode<Button>("HBoxContainer/Exit");

		exitButton.Connect("pressed", new Callable(this, nameof(ExitBattle)));
	}
	
	void ExitBattle()
	{
		(root as Battle).LeaveBattle();
	}
}
