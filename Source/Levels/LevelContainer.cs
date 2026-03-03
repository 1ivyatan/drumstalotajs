using Godot;
using System;

namespace Drumstalotajs.Levels
{
	public partial class LevelContainer : CenterContainer
	{
		private void SpawnLevelButton(Resources.Levels.LevelProps levelProps)
		{
			/*Projectile projectile = ResourceLoader.Load<PackedScene>("res://Scenes/Battle/Map/Projectile.tscn").Instantiate() as Projectile;
			projectile.Set(device);
			AddChild(projectile);*/
			
			LevelMarker levelMarker = ResourceLoader.Load<PackedScene>("res://Scenes/Levels/LevelMarker.tscn").Instantiate() as LevelMarker;
			levelMarker.SetMarker(levelProps);
			AddChild(levelMarker);
		}
		
		public void LoadLevels(Resources.Levels.LevelPack levelPack)
		{
			foreach (Resources.Levels.LevelProps levelProps in levelPack.Levels)
			{
				SpawnLevelButton(levelProps);
			}
		}
	}
}
