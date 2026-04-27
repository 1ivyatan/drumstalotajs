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
	[Signal] public delegate void EditedEventHandler();
	
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
					Selector.Mode = SelectorMode.Locked;
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
	
	public MapResource CurrentLoadedResource { get; private set; } = null;
	
	public MapResource Export()
	{
		return new MapResource(this);
	}
	
	public async void Load(string mapResourcePath)
	{
		State = MapState.Loading;
		try {
			var data = Files.SafeLoadResource<MapResource>(mapResourcePath, false);
			await GroundLayer.Load(data.GroundLayer);
			await DecorationLayer.Load(data.DecorationLayer);
			await EntityLayer.Load(data.EntityLayer);
			await OverlayLayer.Load(data.OverlayLayer);
			CurrentLoadedResource = data;
			Camera.Calibrate();
			State = MapState.Done;
		} catch (Exception e)
		{
			GD.Print(e);
			State = MapState.Error;
		}
	}
	
	public Vector2I ViewportMouseToMap()
	{
		/*
		Vector2 mouseLocalPos = GroundLayer.GetLocalMousePosition();
		Vector2I mapPos = GroundLayer.LocalToMap(mouseLocalPos);
		Rect2I usedRect = GroundLayer.GetUsedRect();
		if (!usedRect.HasArea()) return mapPos;
		return new Vector2I(
			Mathf.Clamp(mapPos.X, usedRect.Position.X, usedRect.End.X - 1),
			Mathf.Clamp(mapPos.Y, usedRect.Position.Y, usedRect.End.Y - 1)
		);*/
		Vector2 mouseLocalPos = GroundLayer.GetLocalMousePosition();
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
				
				if (!(atlasLayer is GroundLayer groundLayer) && IsEmpty(position))
				{
					return;
				}
					
				atlasLayer.AddTile(position, coords);
				added = true;
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
	
	public bool IsEmpty(Vector2I position)
	{
		return GroundLayer.GetCellAtlasCoords(position) == Constants.Vector2I.Negative;
	}
	
	public FilteredTiles Flash(Vector2I position)
	{
		return Selector.GetTiles(position);
	}
	
	private void EmitEdit()
	{
		if (State != MapState.Loading && Mode == MapMode.Editing)
		{
			EmitSignal(SignalName.Edited);
		}
	}
}
