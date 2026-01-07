using Godot;
using System;

public partial class Device : Entity
{
	protected override EntityType Type
	{
		get;
	} = EntityType.Device;
	
	public int Azimuth
	{
		get;
		set => field = (value > 360) ? (value - (360 * (value / 360))) : value;
	} = 0;
}
