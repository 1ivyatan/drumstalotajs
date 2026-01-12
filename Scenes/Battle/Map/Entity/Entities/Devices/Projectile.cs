using Godot;
using System;

public partial class Projectile : Node2D
{
	public float Azimuth
	{
		get;
		private set;
	} = 0;
	
	public float Angle
	{
		get;
		private set;
	} = 0;
	
	public Vector2 TargetPosition
	{
		get;
		private set;	
	}
	
	public void SetTrajectory(float azimuth, Vector2 spawnPosition)
	{
		this.Azimuth = azimuth;
		
		this.Position = new Vector2(spawnPosition.X, spawnPosition.Y);
		this.RotationDegrees = azimuth;
	}
	
	public void Fire()
	{
		this.TargetPosition = new Vector2(200, 200);
		//float distance = GlobalTransform.Origin.DistanceTo(this.TargetPosition);
	}
	
	
	public override void _PhysicsProcess(double delta)
	{
			//this.body.Velocity = Position.DirectionTo(_target) * 400;
			//GD.Print(this.body.Velocity);
			Position += Transform.X * (float)delta * 200;
			
			
			
		// LookAt(_target);
	}
	
	
	
	
	public override void _Ready()
	{
		Fire();
	}
}
