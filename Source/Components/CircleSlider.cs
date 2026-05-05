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
	
	[ExportGroup("Internals")]
	[Export] private Control _buttOfArrow;
	[Export] private TextureRect _arrow;
	
	private bool _pressed = false;
	private bool _clicked = false;
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouse mouseEvent)
		{
			if (mouseEvent is InputEventMouseButton mouseButton)
			{
				if (mouseButton.Pressed && !_clicked)
				{
					var localPos = GetLocalMousePosition();
					var rect = new Rect2(Vector2.Zero, GetRect().Size);
					if (rect.HasPoint(localPos))
					{
						_clicked = true;
					}
				} else if (!mouseButton.Pressed)
				{
					_clicked = false;
					_pressed = false;
				}
				
				if (_clicked)
				{
					_pressed = mouseButton.Pressed;
				}
				
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
			EmitSignal(SignalName.ValueChanged, Value);
		}
	}
	
	public void SetValue(double value)
	{
		var cut = Mathf.Clamp(value, MinValue, MaxValue);
		Value = cut;
		var rads = ((Value - MinValue) / (MaxValue - MinValue) * (2 * Mathf.Pi));
		
		rads -= Mathf.Pi / 2;
		if (rads < 0)//>= Mathf.Tau)
		{
			rads += Mathf.Tau;
		}
		
		_arrow.Rotation = (float)rads;
		EmitSignal(SignalName.ValueChanged, Value);
	}
}
