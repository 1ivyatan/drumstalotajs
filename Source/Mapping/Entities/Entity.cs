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
		set {
			field = ((value % 360) + 360) % 360;
		}
	}
	public double Integrity { get; 
		set { 
			field = Mathf.Clamp(value, 0, 100);
			if (field <= 0) Die();
		}
	} = 100;
	
	public bool Player { get; set; } = false;
	
	/* simplistic, inheritor will do this  in more sophisiscated ways */
	public void DecreaseIntegrity(double factor)
	{
		Integrity -= factor;
	}
}
