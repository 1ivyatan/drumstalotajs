using Godot;
using System;

namespace Drumstalotajs.Battle.Map.Projectiles
{
	public partial class Projectile : Node2D
	{
		[Signal] public delegate void LandedEventHandler();
		
		private Map.Layers.GroundLayer _groundLayer;
		private Map.Layers.EntityLayer _entityLayer;
		
	}
}
