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
		
		public void SetRange(double min, double max, double value)
		{
			angleSlider.MaxValue = max;
			angleSlider.MinValue = min;
			angleSlider.Value = value;
		}
	
		public override void _Ready()
		{
			angleSlider = GetNode<Slider>("Slider");
			label = GetNode<Label>("Degrees");
			
			angleSlider.Connect("value_changed", Callable.From(
			(float value) => {
				label.Text = $"~{value}deg";
				EmitSignal("Change", value);
			}));
		}
	}
}
