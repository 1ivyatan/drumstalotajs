using Godot;
using System;

namespace drumstalotajs.Mapping;

public partial class Map : Node2D
{
	public bool Loaded { get;
		private set
		{
			
		}
	} = false;
	public enum MapMode { LOCK, VIEW, EDIT }
	
	public MapMode Mode { 
		get;
		set
		{
			field = value;
			switch (value)
			{
				case MapMode.LOCK:
					if (Selector != null) Selector.Mode = Selection.Selector.SelectorMode.LOCK;
					if (Camera != null) Camera.Mode = Mapping.Camera.MapCamera.MapCameraMode.LOCK;
					break;
				case MapMode.VIEW: 
					if (Selector != null) Selector.Mode = Selection.Selector.SelectorMode.VIEW;
					if (Camera != null) Camera.Mode = Mapping.Camera.MapCamera.MapCameraMode.VIEW;
					break;
				case MapMode.EDIT:
					if (Selector != null) Selector.Mode = Selection.Selector.SelectorMode.EDIT;
					if (Camera != null) Camera.Mode = Mapping.Camera.MapCamera.MapCameraMode.VIEW;
					break;
				default: break;
			}
		}
	} = MapMode.LOCK;
}
