using Godot;
using System;

public partial class Device : Entity
{
	protected override EntityType Type
	{
		get;
	} = EntityType.Device;
	
	public float Azimuth
	{
		get;
		set {
			if (value > 360)
			{
				field = value - (360 * (int)(value / 360));
			} else if (value < 0)
			{
				field = 360 - value;
			} else
			{
				field = value;
			}
		}
	} = 0;
	
	public float Angle
	{
		get;
		set {
			if (value >= 90)
			{
				field = 89;
			} else if (value < 0)
			{
				field = 0;
			} else
			{
				field = value;
			}
		}
	} = 45;
	
	public void Fire()
	{
		Projectile projectile = ResourceLoader.Load<PackedScene>("res://Scenes/Battle/Map/Entity/Entities/Devices/Projectile.tscn").Instantiate() as Projectile;
		
		projectile.SetTrajectory(this.Azimuth, this.Position);
		
		this.parent.AddChild(projectile);
	}
}
