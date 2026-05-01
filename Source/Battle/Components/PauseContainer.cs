using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Battle.Components;

public partial class PauseContainer : Control
{
	[Signal] public delegate void PressedResumeEventHandler();
	[Signal] public delegate void PressedRestartEventHandler();
	[Signal] public delegate void PressedExitEventHandler();
	
	[Export] private Button _resume;
	[Export] private Button _restart;
	[Export] private Button _exit;

	public override void _Ready()
	{
		_resume.Pressed += () => { EmitSignal(SignalName.PressedResume); };
		_restart.Pressed += () => { EmitSignal(SignalName.PressedRestart); };
		_exit.Pressed += () => { EmitSignal(SignalName.PressedExit); };
	}
}
