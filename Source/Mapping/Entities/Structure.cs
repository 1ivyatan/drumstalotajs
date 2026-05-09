using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Structure : Entity
{
	[Export] private Flag _flag;
	
	[Export] public override EntityPropertiesData Properties { get; 
		set
		{
			field = value;
		}
	}
	
	public override double Integrity { get; 
		set { 
			field = Mathf.Clamp(value, 0, 100);
			if (field <= 0) Disable();
		}
	} = 100;

	public override bool Player { get; set; } = false;
	public override bool Target { get; set; } = false;
	public override bool Disabled { get; set; } = false;
}
