using Godot;
using System;

public partial class DevicePlacingFooter : Widget
{
	Button exitButton;
	Button startBattleButton;
		
	protected override void LoadWidget()
	{
		exitButton = GetNode<Button>("Buttons/Exit");
		startBattleButton = GetNode<Button>("Buttons/StartBattle");

		exitButton.Connect("pressed", new Callable(this, nameof(ExitBattle)));
		startBattleButton.Connect("pressed", new Callable(this, nameof(StartBattle)));
	}
	
	public void UpdateLock(bool enabled)
	{
		startBattleButton.Disabled = !enabled;
	}
	
	void StartBattle()
	{
		(root as Battle).StartBattle();
	}
	
	void ExitBattle()
	{
		(root as Battle).LeaveBattle();
	}
}
