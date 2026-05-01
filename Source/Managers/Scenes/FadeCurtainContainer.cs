using Godot;
using System;
using Drumstalotajs;
using System.Threading.Tasks;

namespace Drumstalotajs.Managers.Scenes;

public partial class FadeCurtainContainer : CanvasLayer
{
	[Export] private double Time { get; set; }
	[Export] private Color FadeInColor { get; set; }
	[Export] private Color FadeOutColor { get; set; }
	[Export] private ColorRect ColorRect { get; set; }
	
	public async Task FadeIn()
	{
		Tween tween = GetTree().CreateTween();
		Visible = true;
		tween.TweenProperty(ColorRect, "color", FadeInColor, (float)Time);
		await ToSignal(tween, Tween.SignalName.Finished);
	}
	
	public async Task FadeOut()
	{
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(ColorRect, "color", FadeOutColor, (float)Time);
		tween.TweenCallback(Callable.From(() => { Visible = false; }));
		await ToSignal(tween, Tween.SignalName.Finished);
	}
}
