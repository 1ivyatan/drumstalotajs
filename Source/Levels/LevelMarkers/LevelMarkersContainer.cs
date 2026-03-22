using Godot;
using System;

namespace drumstalotajs.Levels.LevelMarkers;

public partial class LevelMarkersContainer : Control
{
	[Export] private PackedScene markerScene;
	[Signal] public delegate void ClickedMarkerEventHandler(Resources.Sets.Levels.LevelProperties levelProps);
	
	public void LoadMarkers(Resources.Sets.Levels.LevelProperties[] levels)
	{
		foreach (Resources.Sets.Levels.LevelProperties levelProps in levels)
		{
			SpawnMarker(levelProps);
		}
	}
	
	private void SpawnMarker(Resources.Sets.Levels.LevelProperties levelProps)
	{
		Marker marker = markerScene.Instantiate() as Marker;
		marker.LoadMarker(levelProps);
		marker.Selected += (Resources.Sets.Levels.LevelProperties levelProps) => {
			EmitSignal(SignalName.ClickedMarker, levelProps);
		};
		AddChild(marker);
	}
}
