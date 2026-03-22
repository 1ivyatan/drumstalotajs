using Godot;
using System;

namespace drumstalotajs.Levels.LevelMarkers;

public partial class Marker : TextureButton
{
	[Signal] public delegate void SelectedEventHandler(Resources.Sets.Levels.LevelProperties levelProps);
	private Resources.Sets.Levels.LevelProperties LevelProperties { get; set; }
	
	public override void _Ready()
	{
		Pressed += () => {
			EmitSignal(SignalName.Selected, LevelProperties);
		};
	}
	
	public void LoadMarker(Resources.Sets.Levels.LevelProperties levelProps)
	{
		LevelProperties = levelProps;
		Position = levelProps.Position;
	}
}
