using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Entity : SceneTile
{
	[Export] public virtual EntityPropertiesData Properties { get; set; }
	
	public virtual double Azimuth { get;
		set {
			field = ((value % 360) + 360) % 360;
		}
	} = -1;

	public double Integrity { get; 
		set { 
			field = Mathf.Clamp(value, 0, 100);
			if (field <= 0) Disable();
		}
	} = 100;

	public virtual bool Player { get; set; } = false;
	public virtual bool Target { get; set; } = false;
	public virtual bool Disabled { get; set; } = false;
	
	public virtual void Disable()
	{
		Disabled = true;
	}
	
	/* simplistic, inheritor will do this  in more sophisiscated ways */
	public void DecreaseIntegrity(double amount)
	{
		Integrity -= amount;
	}
}
