using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Managers.Toasts;

public partial class Toast : PanelContainer
{
	[Export] private Label _label;
	
	private double Time { get; set; }
	private string Text { get; set; }
	
	public void StartTimer()
	{
		SceneTreeTimer timer = GetTree().CreateTimer((float)Time);
		timer.Timeout += () => {
			Tween tween = GetTree().CreateTween();
			tween.TweenProperty(this, "modulate:a", 0.0f, 1.0f);
			tween.TweenCallback(Callable.From(QueueFree));
		};
	}
	
	public void AddData(String text, double time)
	{
		Text = text;
		_label.Text = text;
		Time = time;
	}
}
