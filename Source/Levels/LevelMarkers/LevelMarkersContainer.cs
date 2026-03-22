using Godot;
using System;

namespace drumstalotajs.Levels.LevelMarkers;

public partial class LevelMarkersContainer : Control
{
	[Export] private PackedScene markerScene;
	[Signal] public delegate void ClickedMarkerEventHandler(Resources.Sets.Levels.LevelProperties levelProps);
	[Signal] public delegate void UnclickedMarkerEventHandler();
	
	private Marker SelectedMarker { get; set; }
	
	public override void _Ready()
	{
		SelectedMarker = null;
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouse mouseEvent &&
			mouseEvent is InputEventMouseButton mouseClick &&
			mouseClick.ButtonIndex == MouseButton.Left && SelectedMarker != null
		)
		{
			SelectedMarker = null;
			EmitSignal(SignalName.UnclickedMarker);
		}
		
	}
	
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
			SelectedMarker = marker;
			EmitSignal(SignalName.ClickedMarker, levelProps);
		};
		AddChild(marker);
	}
}
