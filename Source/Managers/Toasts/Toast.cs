using Godot;
using System;

namespace Drumstalotajs.Managers.Toasts;

public partial class Toast : PanelContainer
{
	private double Time { get; set; }
	private string Text { get; set; }
	private Label _label;
	
	public override void _Ready()
	{
		SceneTreeTimer timer = GetTree().CreateTimer((float)Time);
		timer.Timeout += () => {
			Tween tween = GetTree().CreateTween();
			tween.TweenProperty(this, "modulate:a", 0.0f, 1.0f);
			tween.TweenCallback(Callable.From(QueueFree));
		};
	}
	
	public Toast(String text, double time)
	{
		_label = new Label();
		Text = text;
		_label.Text = text;
		Time = time;
		AddChild(_label);
	}
}
