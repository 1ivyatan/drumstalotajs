using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;

namespace Drumstalotajs.Battle.Components;

public partial class BattleTopnav : Topnav
{
	[Signal] public delegate void PressedPauseEventHandler();
	[Signal] public delegate void RulerToggledEventHandler(bool toggle);
	[Signal] public delegate void DebugToggledEventHandler(bool toggle);
	
	[Export] private Button _pause;
	[Export] private BaseButton _ruler;
	[Export] private Button _debug;

	public override void _Ready()
	{
		_pause.Pressed += () => { EmitSignal(SignalName.PressedPause); };
		_ruler.Toggled += (bool toggle) => { EmitSignal(SignalName.RulerToggled, toggle); };
		_debug.Toggled += (bool toggle) => { EmitSignal(SignalName.DebugToggled, toggle); };
	}
	
	public void TogglePauseButton(bool toggle)
	{
		_pause.Visible = toggle;
	}
	
	public void ToggleRulerButton(bool toggle)
	{
		_ruler.Visible = toggle;
	}
	
	public void ToggleDebugButton(bool toggle)
	{
		if (!toggle) _debug.Visible = toggle;
		else Utilities.Debug.DebugControl(_debug);
	}
}
