using Godot;
using System;

namespace Drumstalotajs.Resources.Entities
{
	[GlobalClass]
	public partial class Device : Entity
	{
		[ExportGroup("Device")]
		[Export] public double StartingAngle { get; set; }
		[Export] public double AngleRadius { get; set; }
		[Export] public double TraverseRadius { get; set; }
		[Export] public double MuzzleVelocity { get; set; }
		
		[ExportGroup("Projectile")]
		[Export] public double CasingWeight { get; set; }
		[Export] public double ExplosiveWeight { get; set; }
		[Export] public double TntFactor { get; set; }
		
	}
}
