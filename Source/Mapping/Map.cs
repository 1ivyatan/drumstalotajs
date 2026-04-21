using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Resources.Mapping;
using Drumstalotajs.Mapping.Projectiles;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Mapping.Cameras;
using Drumstalotajs.Resources.Mapping.Layers;

namespace Drumstalotajs.Mapping;

public partial class Map : Node2D
{
	[Signal] public delegate void StateChangeEventHandler(MapState state);
	[Signal] public delegate void ModeChangeEventHandler(MapMode mode);
	
	[Export] public GroundLayer GroundLayer { get; private set; }
	[Export] public AtlasLayer DecorationLayer { get; private set; }
	[Export] public EntityLayer EntityLayer { get; private set; }
	[Export] public OverlayLayer OverlayLayer { get; private set; }
	[Export] public ProjectileLayer ProjectileLayer { get; private set; }
	[Export] public Selector Selector { get; private set; }
	[Export] public Camera Camera { get; private set; }
	
	public MapState State { get; 
		private set {
			field = value;
			EmitSignal(SignalName.StateChange, (int)field);
		}
	} = MapState.Initialized;
	
	public MapMode Mode { get;
		set {
			if (State == MapState.Loading || State == MapState.Initialized) return;
			field = value;
			switch (field)
			{
				case MapMode.Locked:
					Selector.Mode = SelectorMode.Invisible;
					Camera.Mode = CameraMode.Locked;
					break;
				case MapMode.HiddenInteractable:
					Selector.Mode = SelectorMode.Invisible;
					Camera.Mode = CameraMode.DragOnly;
					break;
				case MapMode.Interactable:
					Selector.Mode = SelectorMode.Visible;
					Camera.Mode = CameraMode.DragOnly;
					break;
				case MapMode.Editing:
					Selector.Mode = SelectorMode.Visible;
					Camera.Mode = CameraMode.DragOnly;
					break;
				default: break;
			}
			EmitSignal(SignalName.ModeChange, (int)field);
		}
	} = MapMode.Locked;
	
	public MapResource Export()
	{
		return new MapResource(this);
	}
	
	public void Load(MapMeta mapMeta)
	{
		Load(mapMeta.MapResourcePath);
	}
	
	public void Load(string mapResourcePath)
	{
		State = MapState.Loading;
		try {
			var data = Files.SafeLoadResource<MapResource>(mapResourcePath);
			GroundLayer.Load(data.GroundLayer);
			DecorationLayer.Load(data.DecorationLayer);
			EntityLayer.Load(data.EntityLayer);
			OverlayLayer.Load(data.OverlayLayer);
			State = MapState.Done;
		} catch (Exception e)
		{
			GD.Print(e);
			State = MapState.Error;
		}
	}
}
