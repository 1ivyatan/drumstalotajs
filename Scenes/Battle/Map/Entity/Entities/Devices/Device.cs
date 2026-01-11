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
		set {
			if (value > 360)
			{
				field = value - (360 * (value / 360));
			} else if (value < 0)
			{
				field = 360 - value;
			} else
			{
				field = value;
			}
		}
	} = 0;
	
	public void Fire()
	{
		Node projectile = ResourceLoader.Load<PackedScene>("res://Scenes/Battle/Map/Entity/Entities/Devices/Projectile.tscn").Instantiate();
		
		this.parent.AddChild(projectile);
	}
}
