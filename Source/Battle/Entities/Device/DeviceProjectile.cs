using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Device : Entity
	{
		public struct DeviceProjectile
		{
			public double Caliber { get; }
			public double DragCoefficient { get; }
			public double BallisticCoefficient { get;}
			public double TotalWeight { get; }
			public double CasingWeight { get; }
			public double ExplosiveWeight { get; }
			public double TntFactor { get; }
			public double Area { get; }
			public double Radius { get; }
			
			public DeviceProjectile(DeviceProperties deviceProperties, Resources.Entities.Projectile projectileData)
			{
				Caliber = projectileData.Caliber;
				DragCoefficient = projectileData.DragCoefficient;
				BallisticCoefficient = projectileData.BallisticCoefficient;
				CasingWeight = projectileData.CasingWeight;
				ExplosiveWeight = projectileData.ExplosiveWeight;
				TotalWeight = CasingWeight + ExplosiveWeight;
				TntFactor = projectileData.TntFactor;
				Radius = (Caliber / 1000.0) / 2.0;
				Area = Math.PI * Math.Pow(Radius, 2);
			}
		}
	}
}
