using Godot;
using System;

namespace drumstalotajs.Mapping.Camera;

public partial class MapCamera : Camera2D
{
	[Signal] public delegate void StateChangeEventHandler(MapCameraState state);
	public enum MapCameraState { IDLE, ZOOM, DRAG, ZOOMDRAG }
	public MapCameraState State { 
		get; 
		private set
		{
			field = value;
			EmitSignal("StateChange", (int)value);
		}
	} = MapCameraState.IDLE;
}
