using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Device : Entity
	{
		public struct DeviceProjectile
		{
			public struct ProjectileProperties
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
			
				public ProjectileProperties(DeviceProperties deviceProperties, Resources.Entities.Projectile projectileData)
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
			
			public struct BlastProperties
			{
				public double LethalRadius { get; }
				public double CasualityRadius { get; }
				
				public BlastProperties(ProjectileProperties projectileProperties)
				{
					double tntEquivalent = projectileProperties.TntFactor * projectileProperties.ExplosiveWeight;
					LethalRadius = 2.5 * Math.Pow(projectileProperties.CasingWeight * tntEquivalent, 1/3);
					CasualityRadius = 5 * Math.Pow(projectileProperties.CasingWeight * tntEquivalent, 1/3);
				}
			}
			
			public ProjectileProperties Projectile { get; }
			public BlastProperties Blast { get; }
			
			public DeviceProjectile(DeviceProperties deviceProperties, Resources.Entities.Projectile projectileData)
			{
				Projectile = new ProjectileProperties(deviceProperties, projectileData);
				Blast = new BlastProperties(Projectile);
			}
		}
	}
}
