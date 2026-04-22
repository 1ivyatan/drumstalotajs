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
	[Signal] public delegate void EditedEventHandler(MapMode mode);
	
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
					Selector.Mode = SelectorMode.Interactable;
					Camera.Mode = CameraMode.DragOnly;
					break;
				case MapMode.Editing:
					Selector.Mode = SelectorMode.Editing;
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
	
	public async void Load(string mapResourcePath)
	{
		State = MapState.Loading;
		try {
			var data = Files.SafeLoadResource<MapResource>(mapResourcePath);
			await GroundLayer.Load(data.GroundLayer);
			await DecorationLayer.Load(data.DecorationLayer);
			await EntityLayer.Load(data.EntityLayer);
			await OverlayLayer.Load(data.OverlayLayer);
			State = MapState.Done;
		} catch (Exception e)
		{
			GD.Print(e);
			State = MapState.Error;
		}
	}
	
	public Vector2I ViewportToMap()
	{
		Vector2 mouseScreenPos = GetViewport().GetMousePosition();
		Vector2 mouseWorldPos = Camera.ScreenToWorld(mouseScreenPos);
		Vector2 mouseLocalPos = GroundLayer.ToLocal(mouseWorldPos);
		return GroundLayer.LocalToMap(mouseLocalPos);
	}
	
	public void AddTile(
		BaseLayer layer, 
		string atlas,
		Vector2I position
	)
	{
		bool added = false;
		if (layer is AtlasLayer atlasLayer)
		{
			if (Types.Vector2I.ValidVector2I(atlas))
			{
				Vector2I coords = Types.Vector2I.StringToVector2I(atlas);
				
				if (atlasLayer is GroundLayer groundLayer)
				{
					groundLayer.AddTile(position, coords);
					added = true;
				} else
				{
					if (GroundLayer.GetCellAtlasCoords(position) != Constants.Vector2I.Negative)
					{
						atlasLayer.AddTile(position, coords);
						added = true;
					}
				}
			}
		} else if (layer is SceneLayer sceneLayer)
		{
			if (GroundLayer.GetCellAtlasCoords(position) != Constants.Vector2I.Negative)
			{
				sceneLayer.AddTile(position, atlas);
				added = true;
			}
		}
		
		if (added)
		{
			EmitEdit();
		}
	}
	
	public void RemoveTile(BaseLayer layer, Vector2I position)
	{
		if (layer is AtlasLayer atlasLayer)
		{
			atlasLayer.RemoveTile(position);
		} else if (layer is SceneLayer sceneLayer)
		{
			sceneLayer.RemoveTile(position);
		}
	}
	
	private void EmitEdit()
	{
		if (State != MapState.Loading && Mode == MapMode.Editing)
		{
			EmitSignal(SignalName.Edited);
		}
	}
}
