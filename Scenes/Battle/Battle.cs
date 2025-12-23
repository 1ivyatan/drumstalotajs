using Godot;
using System;

public partial class Battle : Node
{
	[Signal]
	public delegate void LevelSelectEventHandler();
	
	public override void _Ready()
	{
		Button pauseButton = GetNode<Button>("FooterContainer/ButtonContainer/Exit");
		
		pauseButton.Connect("pressed", new Callable(this, nameof(ToLevelSelect)));
	}

	void ToLevelSelect() {
		EmitSignal(SignalName.LevelSelect);
	}
}
