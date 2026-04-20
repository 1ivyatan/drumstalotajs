using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Entity : SceneTile
{
	[Export] public EntityPropertiesData Properties { get; private set; }
	public double Azimuth { get; 
		/*!!!!!!!*/
		set {
			field = value;
			
			if (field < 0) Die();
		}
	}
	public double Integrity { get; 
		private set { 
			field = Mathf.Clamp(value, 0, 100);
		}
	} = 100;
	
	
}
