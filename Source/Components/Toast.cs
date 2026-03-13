using Godot;
using System;

namespace drumstalotajs.Components;

public partial class Toast : Control
{
	public string Text { get; private set; }
	public double Time { get; private set; } = 5;
	private Label label;
	
	public override void _Ready()
	{
		label = GetNode<Label>("Label");
		label.Text = Text;
		SceneTreeTimer timer = GetTree().CreateTimer((float)Time);
		timer.Timeout += FadeAndDestoy;
	}
	
	private void FadeAndDestoy()
	{
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "modulate:a", 0.0f, 1.0f);
		tween.TweenCallback(Callable.From(Destroy));
	}
	
	public void SetParams(string text)
	{
		Text = text;
	}
	
	public void Destroy()
	{
		QueueFree();
	}
}
