using Godot;
using System;

public partial class Battle : Control
{
	[Signal]
	public delegate void LevelSelectEventHandler();
	
	public void LeaveBattle()
	{
		EmitSignal(SignalName.LevelSelect);
	}
}
