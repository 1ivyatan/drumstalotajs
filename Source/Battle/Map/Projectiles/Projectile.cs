using Godot;
using System;

namespace Drumstalotajs.Battle.Map.Projectiles
{
	public partial class Projectile : Node2D
	{
		[Signal] public delegate void LandedEventHandler();
		
		public ProjectileProperties Properties { get; private set; }
		private bool Flying { get; set; } = false;
		
		private Map.Layers.GroundLayer _groundLayer;
		private Map.Layers.EntityLayer _entityLayer;
		
		public void Set(Entities.Device device)
		{
			Properties = new ProjectileProperties(device);
		}
		
		public bool HasHit()
		{
			return false;
		}
		
		public void Launch()
		{
			Flying = true;
			Visible = true;
		}
		
		public override void _PhysicsProcess(double delta)
		{
			if (Flying)
			{
				if (HasHit())
				{
					
				} else
				{
					Position = Properties.Position;
					//GD.Print();
					Properties.NextStep(delta);
				}
			}
		}
		
		public override void _Ready()
		{
			Visible = false;
		}
	}
}
