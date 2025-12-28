using Godot;
using System;

public partial class DevicePlacingFooter : Widget
{
	Button startBattleButton;
		
	protected override void LoadWidget()
	{
		startBattleButton = GetNode<Button>("Buttons/StartBattle");
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
}
