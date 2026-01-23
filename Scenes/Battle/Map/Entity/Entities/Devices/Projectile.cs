using Godot;
using System;

public partial class Projectile : Area2D
{
	private ProjectileMotion projectileMotion;

	public bool Flying
	{
		get;
		private set;
	}
	
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
		tween.TweenCallback(Callable.From(this.QueueFree));
		//this.Flying = true;
		//float distance = GlobalTransform.Origin.DistanceTo(this.TargetPosition);
	}
	
	
	public override void _PhysicsProcess(double delta)
	{
		//if (this.Flying)
		//{
			//this.GlobalPosition = this.GlobalPosition.MoveToward(this.projectileMotion.MapMovement.EndPosition, (float)delta);
			//GD.Print(this.projectileMotion.MapMovement.Range * delta);
			//Vector2 velocity = new Vector2(targetDistance.X * (float)delta, targetDistance.Y * (float)delta);
		//	GD.Print(velocity);
			//this.Position += this.projectileMotion.MapMovement.Range * delta;
	//	}
			//this.body.Velocity = Position.DirectionTo(_target) * 400;
		//GD.Print(GlobalPosition.DistanceTo(this.TargetPosition));
		// LookAt(_target);
	}
	
	
	
	
	public override void _Ready()
	{
		GD.Print(this.projectileMotion.Movement.Range);
		Fire();
	}
}
