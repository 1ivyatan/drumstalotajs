using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Battle.Components;

public partial class PauseOverlay : Control
{
	[Signal] public delegate void PressedResumeEventHandler();
	[Signal] public delegate void PressedRestartEventHandler();
	[Signal] public delegate void PressedExitEventHandler();

	[Export] private PauseContainer _pauseContainer;
	
	public override void _Ready()
	{
		_pauseContainer.PressedResume += () => { EmitSignal(SignalName.PressedResume); };
		_pauseContainer.PressedRestart += () => { EmitSignal(SignalName.PressedRestart); };
		_pauseContainer.PressedExit += () => { EmitSignal(SignalName.PressedExit); };
	}
}
