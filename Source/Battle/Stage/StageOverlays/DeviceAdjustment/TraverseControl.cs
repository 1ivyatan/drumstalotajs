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
		
		public void SetRange(double value, double min, double max, bool locked)
		{
			_traverseSlider.Value = value;
			
			if (locked)
			{
				
			} else
			{
				
			}
			
			_degrees.Text = $"~{(int)value}deg";
		}
	
		public override void _Ready()
		{
			_traverseSlider = GetNode<Slider>("Slider");
			_degrees = GetNode<Label>("Degrees");
			
			_traverseSlider.Connect("value_changed", Callable.From(
			(float value) => {
				_degrees.Text = $"~{(int)value}deg";
				EmitSignal("Change", value);
			}));
		}
	}
}
