using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Projectile : Node2D
	{
		[Signal]
		public delegate void LandedEventHandler();
		
		private Tween tween = null;
		
		private void Fire()
		{
			tween = CreateTween();
			tween.SetProcessMode(0);
			tween.SetTrans((Tween.TransitionType)1);
			tween.TweenProperty(this, "position", new Vector2(50.0f, 50.0f), 1);
			tween.TweenCallback(Callable.From(Destroy));
		}
		
		private void Destroy()
		{
			EmitSignal(SignalName.Landed);
			QueueFree();
		}
		
		public override void _Ready()
		{
			Fire();
		}
	}
}
