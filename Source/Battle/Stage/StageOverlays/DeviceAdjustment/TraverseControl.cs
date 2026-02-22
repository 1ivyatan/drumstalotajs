using Godot;
using System;

namespace Drumstalotajs.Battle.Stage.StageOverlays.DeviceAdjustment
{
	public partial class TraverseControl : HBoxContainer
	{
		[Signal]
		public delegate void ChangeEventHandler(float value);
		
		private Slider _traverseSlider;
		private Label _label;
		private Label _degrees;
		
		public void SetText(double value)
		{
			_degrees.Text = $"~{(int)value}deg";
		}
		
		public void SetRange(double value, double min, double max, bool locked)
		{
			_traverseSlider.Value = value;
			_traverseSlider.MinValue = min;
			_traverseSlider.MaxValue = max;
			SetText(value);
		}
	
		public override void _Ready()
		{
			_traverseSlider = GetNode<Slider>("Slider");
			_degrees = GetNode<Label>("Degrees");
			
			_traverseSlider.Connect("value_changed", Callable.From(
			(float value) => {
				SetText(value);
				EmitSignal("Change", value);
			}));
		}
	}
}
