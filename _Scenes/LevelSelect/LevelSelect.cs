using Godot;
using System;

public partial class LevelSelect : Control
{
	[Signal]
	public delegate void StartMenuEventHandler();
	
	[Signal]
	public delegate void BattleEventHandler();
	
	public override void _Ready()
	{
		Button exitButton = GetNode<Button>("FooterPanel/ExitButton");
		Button playButton = GetNode<Button>("InfoPanel/VerticalContainer/PlayButton");
		
		exitButton.Connect("pressed", new Callable(this, nameof(ToStartMenu)));
		playButton.Connect("pressed", new Callable(this, nameof(ToBattle)));
	}
	
	void ToStartMenu() {
		EmitSignal(SignalName.StartMenu);
	}
	
	void ToBattle() {
		EmitSignal(SignalName.Battle);
	}
}
