using Godot;
using System;

namespace Drumstalotajs.Battle.Map.Projectiles
{
	public partial class ProjectileManager : Node2D
	{
		private Resources.Levels.Level _levelData;
		private const int _maxShells = 50;
		
		/*
		
		public Projectile Fire()
		{
			Entities.Projectile projectile = ResourceLoader.Load<PackedScene>("res://Scenes/Battle/Entities/Devices/Projectile.tscn").Instantiate() as Projectile;
			Resources.Levels.Level levelData = (GetNode("../../../..") as Battle.Scene).Level;
			Vector2I gridPosition = _groundLayer.LocalToMap(Position);
			TileData tileData = _groundLayer.GetCellTileData(gridPosition);
			projectile.SetProjectile(Properties, Projectile, levelData, tileData);
			_parent.AddChild(projectile);
			return projectile;
		}*/
		
		public Projectile SpawnShell(Entities.Device device)
		{
			Projectile projectile = ResourceLoader.Load<PackedScene>("res://Scenes/Battle/Map/Projectile.tscn").Instantiate() as Projectile;
			AddChild(projectile);
			return projectile;
		}
		
		public override void _Ready()
		{
			_levelData = (GetNode("../../..") as Battle.Scene).Level;
		}
	}
}
