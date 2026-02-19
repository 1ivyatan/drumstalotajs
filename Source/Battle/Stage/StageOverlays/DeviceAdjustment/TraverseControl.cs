using Godot;
using System;

namespace Drumstalotajs.Battle.Stage.StageOverlays.DeviceAdjustment
{
	public partial class TraverseControl : HBoxContainer
	{
		[Signal]
		public delegate void ChangeEventHandler(float value);
		
		private Slider traverseSlider;
		private Label label;
		
		public void SetRange(bool locked, double value)
		{
			traverseSlider.Value = value;
			traverseSlider.Visible = true;

			label.Text = $"~{(int)value}deg";
		}
	
		public override void _Ready()
		{
			traverseSlider = GetNode<Slider>("Slider");
			label = GetNode<Label>("Degrees");
			
			traverseSlider.Connect("value_changed", Callable.From(
			(float value) => {
				label.Text = $"~{(int)value}deg";
				EmitSignal("Change", value);
			}));
		}
	}
}
