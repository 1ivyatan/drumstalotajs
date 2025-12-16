using Godot;
using System;

public partial class StartMenu : HBoxContainer
{
	[Signal]
	public delegate void LevelSelectEventHandler();
	
	[Signal]
	public delegate void ExitEventHandler();
	
	public override void _Ready()
	{
		Button startButton = GetNode<Button>("MenuContainer/Center/ButtonContainer/Start");
		Button exitButton = GetNode<Button>("MenuContainer/Center/ButtonContainer/FooterContainer/Exit");
		
		startButton.Connect("pressed", new Callable(this, nameof(ToLevelSelect)));
		exitButton.Connect("pressed", new Callable(this, nameof(Exit)));
	}
	
	void ToLevelSelect() {
		EmitSignal(SignalName.LevelSelect);
	}
	
	void ToExit() {
		EmitSignal(SignalName.Exit);
	}
}
