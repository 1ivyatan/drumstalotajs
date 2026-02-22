using Godot;
using System;

namespace Drumstalotajs.Resources.Entities
{
	[GlobalClass]
	public partial class Device : Entity
	{
		[Export] public double AngleMin { get; set; }
		[Export] public double AngleMax { get; set; }
		[Export] public double TraverseRange { get; set; }
		[Export] public double MuzzleVelocity { get; set; }
		[Export] public Resources.Entities.Projectile[] Projectiles { get; set; }
	}
}
