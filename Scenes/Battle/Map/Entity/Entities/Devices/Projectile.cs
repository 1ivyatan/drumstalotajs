using Godot;
using System;

public partial class Projectile : Node2D
{
	public float Azimuth
	{
		get;
		private set;
	}
	
	public void SetTrajectory(float azimuth)
	{
		this.Azimuth = azimuth;
	}
}
