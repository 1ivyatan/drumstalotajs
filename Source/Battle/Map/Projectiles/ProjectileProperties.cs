using Godot;
using System;

namespace Drumstalotajs.Battle.Map.Projectiles
{
	public partial class Projectile : Node2D
	{
		public class ProjectileProperties
		{
			public double Altitude { get; private set; }
			public double HVelocity { get; private set; }
			public double VVelocity { get; private set; }
			public double Position { get; private set; }
			
			private Entities.Device _device;
			
			public void NextStep()
			{
				GD.Print("next");
			}
			
			public void Reset()
			{
				//Altitu
			}
			
			public ProjectileProperties(Entities.Device device)
			{
				_device = device;
				Reset();
			}
		}
	}
}
