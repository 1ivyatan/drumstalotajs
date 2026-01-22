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
		
		//this.Flying = true;
		//float distance = GlobalTransform.Origin.DistanceTo(this.TargetPosition);
	}
	
	
	public override void _PhysicsProcess(double delta)
	{
		//if (this.Flying)
		//{
	//		Vector2 targetDistance = (this.TargetPosition - this.Position);
//			Vector2 velocity = new Vector2(targetDistance.X * (float)delta, targetDistance.Y * (float)delta);
//			this.Position += velocity;
//		}
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
