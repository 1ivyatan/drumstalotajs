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
	
	public async void FadeIn(float Delay) {
		Tween tween = CreateTween();
		tween.TweenProperty(tint, "color", Colors.White, Delay);
		
		await ToSignal(tween, "finished");
		
		tween.TweenCallback(Callable.From(tween.Kill));
	}
	
	public async void FadeOut(float Delay) {
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(tint, "color", Colors.Black, Delay);
		
		
		await ToSignal(tween, "finished");
		
		tween.TweenCallback(Callable.From(tween.Kill));
	}
}
