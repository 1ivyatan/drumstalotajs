using Godot;
using System;

namespace Drumstalotajs.Battle.Stage.StageOverlays.DeviceAdjustment
{
	public partial class AngleControl : HBoxContainer
	{
		[Signal]
		public delegate void ChangeEventHandler(float value);
		
		private Slider angleSlider;
		private Label label;
		
		public void SetRange(double value, double min, double max)
		{
			angleSlider.MaxValue = max;
			angleSlider.MinValue = min;
			angleSlider.Value = value;
			label.Text = $"~{(int)value}deg";
		}
	
		public override void _Ready()
		{
			angleSlider = GetNode<Slider>("Slider");
			label = GetNode<Label>("Degrees");
			
			angleSlider.Connect("value_changed", Callable.From(
			(float value) => {
				label.Text = $"~{(int)value}deg";
				EmitSignal("Change", value);
			}));
		}
	}
}
