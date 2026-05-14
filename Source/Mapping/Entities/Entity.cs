using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Entity : SceneTile
{
	[Signal] public delegate void DisabledEntityEventHandler();
	[Export] public virtual EntityPropertiesData Properties { get; set; }
	
	public virtual double Azimuth { get;
		set {
			field = ((value % 360) + 360) % 360;
		}
	} = -1;

	public virtual double Integrity { get; 
		set { 
			field = Mathf.Clamp(value, 0, 100);
			if (field <= 0) Disabled = true;
		}
	} = 100;

	public virtual bool Player { get; 
		set;
	} = false;
	public virtual bool Target { get; set; } = false;
	public virtual bool Disabled { 
		get;
		set
		{
			if (value && field != value)
			{
				EmitSignal(SignalName.DisabledEntity);
			}
			field = value;
		}
	} = false;
	
	/* simplistic, inheritor will do this  in more sophisiscated ways */
	public void DecreaseIntegrity(double amount)
	{
		double dmg = amount;
		double intFactor = Integrity < 50.0 ? 1.0 + (50.0 - Integrity) / 100.0 : 1.0;
		dmg *= intFactor;
		Integrity -= dmg;
	//	GD.Print(Integrity);
	}
}
