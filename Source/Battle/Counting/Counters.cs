using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Components;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Battle.Counting;

public partial class Counters : Node
{
	public TimerDisplay TimerDisplay { get; private set; }
	
	public override void _Ready()
	{
		TimerDisplay = GetNode("TimerDisplay") as TimerDisplay;
	}
}
