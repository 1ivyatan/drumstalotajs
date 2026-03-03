using Godot;
using System;

namespace Drumstalotajs.Levels
{
	public partial class LevelContainer : CenterContainer
	{
		public void LoadLevels(Resources.Levels.LevelPack levelPack)
		{
			foreach (Resources.Levels.LevelProps levelProps in levelPack.Levels)
			{
				GD.Print(levelProps.Position);
			}
		}
	}
}
