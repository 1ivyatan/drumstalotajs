using Godot;
using System;

namespace Drumstalotajs.Resources.Entities
{
	[GlobalClass]
	public partial class Projectile : Resource
	{
		[Export] public double Caliber { get; set; }
		[Export] public double DragCoefficient { get; set; }
		[Export] public double BallisticCoefficient { get; set; }
		[Export] public double CasingWeight { get; set; }
		[Export] public double ExplosiveWeight { get; set; }
		[Export] public double TntFactor { get; set; }
	}
}
