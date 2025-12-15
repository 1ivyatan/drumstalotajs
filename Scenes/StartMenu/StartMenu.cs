using Godot;
using System;

public partial class StartMenu : HBoxContainer
{
	[Signal]
	public delegate void StartMenuStartEventHandler();
	
	[Signal]
	public delegate void StartMenuExitEventHandler();
	
	public override void _Ready()
	{
		Button startButton = GetNode<Button>("MenuContainer/Center/ButtonContainer/Start");
		Button exitButton = GetNode<Button>("MenuContainer/Center/ButtonContainer/FooterContainer/Exit");
		
		startButton.Connect("pressed", new Callable(this, nameof(Start)));
		exitButton.Connect("pressed", new Callable(this, nameof(Exit)));
	}
	
	void Start() {
		EmitSignal(SignalName.StartMenuStart);
	}
	
	void Exit() {
		EmitSignal(SignalName.StartMenuExit);
	}
}
