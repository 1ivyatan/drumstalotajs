using Godot;
using System;
using System.Collections;

public partial class Fade : Control
{
	CanvasModulate tint;
	
	public override void _Ready()
	{
		tint = GetNode<CanvasModulate>("Tint");
	}
	
	public void FadeIn(float Delay) {
		
	}
	
	public void FadeOut(float Delay) {
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(tint, "color", Colors.White, 1.0f);
		tween.TweenCallback(Callable.From(tint.QueueFree));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
