using Godot;
using System;

namespace Drumstalotajs.Battle.Map.Projectiles
{
	public partial class ProjectileManager : Node2D
	{
		private Map.Layers.EntityLayer _entityLayer;
		private Map.Layers.GroundLayer _groundLayer;
		private Resources.Levels.Level _levelData;
		private const int _maxShells = 50;
		
		public Projectile SpawnShell(Entities.Device device)
		{
			if (GetChildCount() >= 50)
			{
				Node oldestProjectile = GetChild(0);
				oldestProjectile.QueueFree();
				RemoveChild(oldestProjectile);
			}
			
			Projectile projectile = ResourceLoader.Load<PackedScene>("res://Scenes/Battle/Map/Projectile.tscn").Instantiate() as Projectile;
			projectile.Set(device);
			AddChild(projectile);
			projectile.Launch();
			return projectile;
		}
		
		public override void _Ready()
		{
			_levelData = (GetNode("../../..") as Battle.Scene).Level;
			_entityLayer = GetNode<Node2D>("../EntityLayer") as Map.Layers.EntityLayer;
			_groundLayer = GetNode<Node2D>("../GroundLayer") as Map.Layers.GroundLayer;
		}
	}
}
