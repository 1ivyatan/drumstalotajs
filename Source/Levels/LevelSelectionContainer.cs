using Godot;
using System;

namespace drumstalotajs.Levels;

public partial class LevelSelectionContainer : Control
{
	private LevelMarkers.LevelMarkersContainer levelMarkersContainer;
	
	public override void _Ready()
	{
		levelMarkersContainer = GetNode<Control>("LevelsContainer") as LevelMarkers.LevelMarkersContainer;
		levelMarkersContainer.ClickedMarker += (Resources.Sets.Levels.LevelProperties levelProps) => {
			GD.Print(111111);
		};
		Visible = false;
	}
	
	public void LoadLevelSelection(Resources.Sets.Levels.LevelSet levelSet)
	{
		levelMarkersContainer.LoadMarkers(levelSet.Levels);
		Visible = true;
	}
}
