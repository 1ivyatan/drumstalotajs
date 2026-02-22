using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Projectile : Area2D
	{
		public struct ProjectileProperties
		{
			public double Angle { get; private set; }
			public double InitialVelocity { get; private set; }
			public double Altitude { get; private set; }
			public double Range { get; private set; }
			public double Time { get; private set; }
			private double _vy0 { get; set; }
			
			public ProjectileProperties(Device.DeviceProperties properties, Device.DeviceProjectile projectile, Resources.Levels.Level levelData, TileData tileData)
			{
				Angle = properties.Angle.Value;
				InitialVelocity = properties.Velocity;
				Altitude = properties.Altitude;
				Range = 
			}
		}
	}
}
