using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Components;

public partial class CircleSlider : Control
{
	[Signal] public delegate void ValueChangedEventHandler(double value);

	[ExportGroup("Range")]
	[Export] public double Value { get; set; } = 0;
	[Export] public double MinValue { get; set; } = 0;
	[Export] public double MaxValue { get; set; } = 100;
	[Export] public double Step { get; set; } = 1;
	
	[ExportGroup("Internals")]
	[Export] private Control _buttOfArrow;
	[Export] private TextureRect _arrow;
	
	private bool _pressed = false;
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouse mouseEvent)
		{
			if (mouseEvent is InputEventMouseButton mouseButton)
			{
				_pressed = mouseButton.Pressed;
			}
		}
		
		if (_pressed)
		{
			var direction = (GetGlobalMousePosition() - _buttOfArrow.GlobalPosition);
			var rot = Mathf.Atan2(direction.Y,direction.X);
			var deg = Mathf.RadToDeg(rot);

			if (deg < 0)
			{
				deg += 360;
				rot += Mathf.Tau;
			}

			rot += Mathf.Pi / 2;
			if (rot >= Mathf.Tau)
			{
				rot -= Mathf.Tau;
			}
			
			_arrow.RotationDegrees = deg;
			Value = rot / (2 * Mathf.Pi) * (MaxValue - MinValue) + MinValue;
			GD.Print("Value: ", Value);
			EmitSignal(SignalName.ValueChanged, Value);
		}
	}
}
