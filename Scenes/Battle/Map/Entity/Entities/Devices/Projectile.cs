using Godot;
using System;

public partial class Projectile : Area2D
{
	[Signal]
	public delegate void ProjectileLandedEventHandler();
	
	private ProjectileMotion projectileMotion;
	
	public void SetTrajectory(float azimuth, float initialVelocity, float angle, Vector2 spawnPosition)
	{
		this.projectileMotion = new ProjectileMotion(spawnPosition, (double)azimuth, (double)angle, (double)initialVelocity);

		this.Position = this.projectileMotion.MapMovement.StartPosition;
		this.Rotation = (float)this.projectileMotion.MapMovement.Rotation;
	}
	
	public void Fire()
	{
		Tween tween = this.CreateTween();
		tween.TweenProperty(this, "position", this.projectileMotion.MapMovement.EndPosition, 1.0f);
		tween.TweenCallback(Callable.From(() => {
			this.EmitSignal(SignalName.ProjectileLanded);
			this.QueueFree();
		}));
	}
	
	public override void _Ready()
	{
		GD.Print(this.projectileMotion.Movement.Range);
		Fire();
	}
}
