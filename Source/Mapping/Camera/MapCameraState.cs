using Godot;
using System;

namespace drumstalotajs.Mapping.Camera;

public partial class MapCamera : Camera2D
{
	public enum MapCameraState { LOCK, VIEW }
	
	public MapCameraState Mode
	{
		get;
		set
		{
			field = value;
			switch (value)
			{
				case MapCameraState.LOCK: 
					break;
				case MapCameraState.VIEW: 
					break;
				default: break;
			}
		}
	}
}
