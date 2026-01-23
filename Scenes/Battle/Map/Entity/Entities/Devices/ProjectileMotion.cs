using Godot;
using System;

public class ProjectileMotion
{
	public class Calculation
	{
		public double Angle
		{
			get;
			private set;
		}
		
		public double Range
		{
			get;
			private set;
		}
			
		public double InitialVelocity
		{
			get;
			private set;
		}
		
		public double Time
		{
			get;
			private set;
		}
		
		public Calculation(double angle, double initialVelocity)
		{
			this.Angle = angle;
			this.InitialVelocity = initialVelocity;
			this.Range = (
				Math.Pow(initialVelocity, 2) * Math.Sin(2 * Physics.ToRadians(this.Angle))
			) / Physics.Gravity;
			this.Time = 2 * ((initialVelocity * Math.Sin(angle) ) / Physics.Gravity);
		}
	}
	
	public class CanvasCalculation
	{
		public double Azimuth
		{
			get;
			private set;
		}
		
		public double Rotation
		{
			get;
			private set;
		}
		
		public Vector2 StartPosition
		{
			get;
			private set;
		}
		
		public Vector2 EndPosition
		{
			get;
			private set;
		}
		
		public Vector2 Range
		{
			get;
			private set;
		}
		
		public CanvasCalculation(Calculation projectile, Vector2 startPosition, double azimuth)
		{
			this.Azimuth = azimuth;
			this.Rotation = (90.0 - azimuth) * (Math.PI / 180.0);
			this.StartPosition = startPosition;
			this.EndPosition = new Vector2(
				(float)(startPosition.X + (projectile.Range * 1) * Math.Cos(this.Rotation)),
				(float)(startPosition.Y + (projectile.Range * 1) * Math.Sin(this.Rotation))
			);
			this.Range = this.EndPosition - this.StartPosition;
		}
	}
	
	public Calculation Movement
	{
		get;
		private set;
	}
	
	public CanvasCalculation MapMovement
	{
		get;
		private set;
	}
	
	public ProjectileMotion(Vector2 startPosition, double azimuth, double angle, double initialVelocity)
	{
		this.Movement = new Calculation(angle, initialVelocity);
		this.MapMovement = new CanvasCalculation(this.Movement, startPosition, azimuth);
	}
}
