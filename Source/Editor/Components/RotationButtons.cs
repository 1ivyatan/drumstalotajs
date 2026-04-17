using Godot;
using System;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Editor.Components;

public partial class RotationButtons : Control
{
	[Signal] public delegate void ClickedRotationEventHandler(double degrees);
	
	[Export] private Button _q1;
	[Export] private Button _q2;
	[Export] private Button _q3;
	[Export] private Button _q4;
	
	public override void _Ready()
	{
		_q1.Pressed += () => EmitRotation(0);
		_q2.Pressed += () => EmitRotation(90);
		_q3.Pressed += () => EmitRotation(180);
		_q4.Pressed += () => EmitRotation(270);
	}
	
	private void EmitRotation(double degrees)
	{
		EmitSignal(SignalName.ClickedRotation, degrees);
	}
}
