using Godot;
using System;

namespace Drumstalotajs.Levels
{
	public partial class LevelContainer : Control
	{
		private void SpawnLevelButton(Resources.Levels.LevelProps levelProps)
		{
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
