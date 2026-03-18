using Godot;
using System;

namespace drumstalotajs.Mapping.Camera;

public partial class MapCamera : Camera2D
{
	public enum MapCameraMode { LOCK, VIEW }
	
	public MapCameraMode Mode
	{
		get;
		set
		{
			field = value;
			switch (value)
			{
				case MapCameraMode.LOCK: 
					break;
				case MapCameraMode.VIEW: 
					break;
				default: break;
			}
		}
	} 
}
