using Godot;
using System;

public partial class Device : Entity
{
	protected override EntityType Type
	{
		get;
	} = EntityType.Device;
	
	private int azimuth;
	public int Azimuth
	{
		get
		{
			return this.Azimuth;
		}
		
		set
		{
			this.azimuth = value > 360 ? value - ( 360 *  ( value / 360 )) : value;
		}
	} = 0;
}
