using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Components;

public partial class Wheel : Control
{
	[Signal] public delegate void ValueChangedEventHandler(double value);
	
	[ExportGroup("Range")]
	[Export] public double Value { get; 
		set {
			field = value;
			EmitSignal(SignalName.ValueChanged, Value);
		}
	} = 0;
	[Export] public double MinValue { get; set; } = -100;
	[Export] public double MaxValue { get; set; } = 100;
	[Export] public double Step { get; set; } = 1;

	[ExportGroup("Textures")]
	[Export] private Texture2D _normalArrow;
	[Export] private Texture2D _hoveredArrow;
	[Export] private Texture2D _wheel;
	
	[ExportGroup("Wheel")]
	[Export] public double WheelSpeed { get; set; } = 250;
	[Export] public double WheelIntensity { get; set; } = 2.5;
	
	[ExportGroup("Internal Nodes")]
	[Export] private Slider _slider;
	[Export] private TextureRect _wheelNode;
	[Export] private TextureRect _left;
	[Export] private TextureRect _right;
	[Export] private AudioStreamPlayer _crankSfx;
	private bool _dragging = false;
	
	public override void _Ready()
	{
		CustomMinimumSize = _wheel.GetSize();
		_left.Texture = _normalArrow;
		_right.Texture = _normalArrow;
		
		//GD.Print(Size);
		
		_wheelNode.PivotOffset = CustomMinimumSize * 0.5f;
		_slider.Modulate = new Color(1, 1, 1, 0); 
		_slider.Size = _wheel.GetSize();
		_slider.MinValue = -WheelIntensity;
		_slider.MaxValue = WheelIntensity;
		_slider.DragStarted += () => {
			_dragging = true;
		};
		_slider.DragEnded += (bool changed) => { 
			StopWheel();
		};
	}
	
	private void TurnWheel(double intensity, double delta)
	{
		if (MinValue < Value && Value < MaxValue)
		{
			_wheelNode.RotationDegrees += ((float)intensity * (float)delta) * (float)WheelSpeed;
		}
	}
	
	private void StopWheel()
	{
		_dragging = false;
		_slider.Value = 0;
		PilotArrow(0);
	}
	
	private void PilotArrow(double direction)
	{
		if (direction == 0)
		{
			_left.Texture = _normalArrow;
			_right.Texture = _normalArrow;
			_right.Visible = true;
			_left.Visible = true;
		} else if (direction > 0)
		{
			_right.Texture = _hoveredArrow;
			_right.Visible = true;
			_left.Visible = false;
		} else if (direction < 0)
		{
			_left.Texture = _hoveredArrow;
			_right.Visible = false;
			_left.Visible = true;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_dragging)
		{
			TurnWheel(_slider.Value, delta);
			PilotArrow(_slider.Value);
			Value = Mathf.Clamp(Value + (Math.Sign(_slider.Value) * Step), MinValue, MaxValue);
			if (!_crankSfx.Playing)
			{
				_crankSfx.Play();
			}
		}
	}
}
