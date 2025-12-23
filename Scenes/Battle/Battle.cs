using Godot;
using System;

public partial class Battle : VBoxContainer
{
	[Signal]
	public delegate void LevelSelectEventHandler();
	
	public override void _Ready()
	{
		//Button pauseButton = GetNode<Button>("FooterContainer/ButtonContainer/Exit");
		//pauseButton.Connect("pressed", new Callable(this, nameof(ToLevelSelect)));
	}
	
	void StageChanged(string name)
	{
		GD.Print("it's now " + name);
	}

	void ToLevelSelect() {
		EmitSignal(SignalName.LevelSelect);
	}
}
