using Godot;
using System;

namespace drumstalotajs.Mapping;

public partial class Map : Node2D
{
	public bool Loaded { get; private set; } = false;
	
	public enum MapState { LOCK, VIEW, EDIT }
	
	public MapState Mode { 
		get;
		set
		{
			field = value;
			switch (value)
			{
				case MapState.LOCK:
					if (Selector != null) Selector.Mode = Mapping.Selector.SelectorState.LOCK;
					if (Camera != null) Camera.Mode = Mapping.Camera.MapCameraState.LOCK;
					break;
				case MapState.VIEW: 
					if (Selector != null) Selector.Mode = Mapping.Selector.SelectorState.VIEW;
					if (Camera != null) Camera.Mode = Mapping.Camera.MapCameraState.VIEW;
					break;
				case MapState.EDIT:
					if (Selector != null) Selector.Mode = Mapping.Selector.SelectorState.EDIT;
					if (Camera != null) Camera.Mode = Mapping.Camera.MapCameraState.VIEW;
					break;
				default: break;
			}
		}
	} = MapState.LOCK;
}
