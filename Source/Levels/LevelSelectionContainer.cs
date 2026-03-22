using Godot;
using System;

namespace drumstalotajs.Levels;

public partial class LevelSelectionContainer : Control
{
	public override void _Ready()
	{
		Visible = false;
	}
	
	public void LoadLevelSelection(Resources.Sets.Levels.LevelSet levelSet)
	{
		Visible = true;
	}
}
