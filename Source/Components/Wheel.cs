using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Components;

public partial class Wheel : Control
{
	[Signal] public delegate void ValueChangedEventHandler(double value);
	
	[ExportGroup("Range")]
	[Export] public double Value { get; set; } = 0;
	[Export] public double MinValue { get; set; } = -100;
	[Export] public double MaxValue { get; set; } = 100;
	[Export] public double Step { get; set; } = 0.5;
	
	[ExportGroup("Wheel")]
	[Export] private double WheelSpeed { get; set; } = 250;
	[Export] private double Direction { get; set; } = 1;
	
	[ExportGroup("Internals")]
	[Export] private TextureRect _wheel;
	[Export] private Control _controls;
	[Export] private BaseButton _leftButton;
	[Export] private BaseButton _rightButton;
	
	private double _wheelDirection = 0;
	
	public override void _Ready()
	{
		_wheel.PivotOffset = _wheel.Texture.GetSize() * 0.5f;
		_controls.Size = _wheel.Texture.GetSize();
		_leftButton.ButtonDown += () => { TurnWheel(-1); };
		_rightButton.ButtonDown += () => { TurnWheel(1); };
		_leftButton.ButtonUp += () => { TurnWheel(0); };
		_rightButton.ButtonUp += () => { TurnWheel(0); };
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if (_wheelDirection != 0)
		{
			Value = Mathf.Clamp(Value + _wheelDirection, MinValue, MaxValue);
			EmitSignal(SignalName.ValueChanged, Value);
			if (MinValue < Value && Value < MaxValue)
			{
				_wheel.RotationDegrees += ((float)_wheelDirection * (float)delta) * (float)WheelSpeed;
			}
		}
	}
	
	private void TurnWheel(double direction)
	{
		_wheelDirection = direction * Direction;
	}
}
