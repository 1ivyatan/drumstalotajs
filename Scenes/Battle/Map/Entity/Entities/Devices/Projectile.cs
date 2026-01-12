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
	
	public void SetTrajectory(float azimuth, Vector2 spawnPosition)
	{
		this.Azimuth = azimuth;
		
		this.Position = new Vector2(spawnPosition.X, spawnPosition.Y);
		this.RotationDegrees = azimuth;
	}
	
	public void Fire()
	{
		
	}
	
	public override void _EnterTree()
	{
		Fire();
	}
}
