using Godot;
using System;

public partial class LevelSelect : Control
{
	[Signal]
	public delegate void StartMenuEventHandler();
	
	public override void _Ready()
	{
		Button exitButton = GetNode<Button>("FooterPanel/ExitButton");
		
		exitButton.Connect("pressed", new Callable(this, nameof(ToStartMenu)));
	}
	
	void ToStartMenu() {
		EmitSignal(SignalName.StartMenu);
	}
}
