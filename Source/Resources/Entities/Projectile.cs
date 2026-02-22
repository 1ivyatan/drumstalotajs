using Godot;
using System;

namespace Drumstalotajs.Resources.Entities
{
	[GlobalClass]
	public partial class Projectile : Resource
	{
		[Export] public double CasingWeight { get; set; }
		[Export] public double ExplosiveWeight { get; set; }
		[Export] public double TntFactor { get; set; }
	}
}
