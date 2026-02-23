using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Device : Entity
	{
		public struct DeviceProjectile
		{
			public double TotalWeight { get; }
			public double CasingWeight { get; }
			public double ExplosiveWeight { get; }
			public double TntFactor { get; }
			
			public DeviceProjectile(DeviceProperties deviceProperties, Resources.Entities.Projectile projectileData)
			{
				CasingWeight = projectileData.CasingWeight;
				ExplosiveWeight = projectileData.ExplosiveWeight;
				TotalWeight = CasingWeight + ExplosiveWeight;
				TntFactor = projectileData.TntFactor;
			}
		}
	}
}
