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
		} = 0;
		
		public double Range
		{
			get;
			private set;
		} = 0;
			
		public double InitialVelocity
		{
			get;
			private set;
		} = 0;
		
		public Calculation(double angle, double initialVelocity)
		{
			this.Angle = angle;
			this.InitialVelocity = initialVelocity;
			this.Range = (
				Math.Pow(initialVelocity, 2) * Math.Sin(2 * Physics.ToRadians(this.Angle))
			) / Physics.Gravity;
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
		
		public CanvasCalculation(Vector2 startPosition, double azimuth)
		{
			this.Azimuth = azimuth;
			this.Rotation = (90.0 - azimuth) * (Math.PI / 180.0);
			this.StartPosition = startPosition;
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
		this.MapMovement = new CanvasCalculation(startPosition, azimuth);
	}
}
