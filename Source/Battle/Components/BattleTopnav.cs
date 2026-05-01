using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;

namespace Drumstalotajs.Battle.Components;

public partial class BattleTopnav : Topnav
{
	[Signal] public delegate void PressedPauseEventHandler();
	
	[Export] private Button _pause;

	public override void _Ready()
	{
		_pause.Pressed += () => { EmitSignal(SignalName.PressedPause); };
	}
}
