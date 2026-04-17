using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Cameras;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Mapping;

public partial class Map : Node2D
{
	[Signal] public delegate void EditedEventHandler();
	
	public MapMode Mode { 
		get; 
		set {
			field = value;
			switch (field)
			{
				case MapMode.Lock:
					Camera.Mode = CameraMode.Lock;
					break;
			}
		}
	} = MapMode.Lock;
	public MapState State { get; private set; } = MapState.Empty;
	
	[Export] public GroundLayer GroundLayer { get; private set; }
	[Export] public DecorationLayer DecorationLayer { get; private set; }
	[Export] public Selector Selector { get; private set; }
	[Export] public Camera Camera { get; private set; }
	
	public override void _Ready()
	{
		Camera.SetCalibratingAtlasLayer(GroundLayer);
		GroundLayer.ChangedLayer += EmitEdit;
	}
	
	private void EmitEdit()
	{
		if (State != MapState.Loading && Mode == MapMode.Edit)
		{
			EmitSignal(SignalName.Edited);
		}
	}
	
	public void AddTile(LayerBase layer, string tileName, Vector2I position)
	{
		bool added = false;
		if (layer is AtlasLayer atlasLayer && Types.Vector2I.ValidVector2I(tileName))
		{
			Vector2I name = Types.Vector2I.StringToVector2I(tileName);
			
			if (layer is GroundLayer groundLayer)
			{
				groundLayer.AddTile(position, name);
				added = true;
			} else
			{
				if (GroundLayer.GetCellAtlasCoords(name) == Types.Vector2I.Negative) return;
				
				if (layer is DecorationLayer decorationLayer)
				{
					decorationLayer.AddTile(position, name);
					added = true;
				}
			} 
		}
		
		if (added)
		{
			EmitEdit();
		}
	}
	
	public void RemoveTile(LayerBase layer, Vector2I position)
	{
		bool removed = false;
		
		if (layer is AtlasLayer atlasLayer)
		{
			if (atlasLayer is GroundLayer groundLayer)
			{
				if (DecorationLayer.GetCellAtlasCoords(position) == Types.Vector2I.Negative)
				{
					groundLayer.RemoveTile(position);
					removed = true;
				}
			} else
			{
				if (atlasLayer is DecorationLayer decorationLayer)
				{
					decorationLayer.RemoveTile(position);
					removed = true;
				}
			}
		}
		
		if (removed)
		{
			EmitEdit();
		}
	}
	
	public MapData Export()
	{
		return new MapData(this);
	}
	
	public void Load(String path)
	{
		State = MapState.Loading;
		
		if (Files.Exists<MapData>(path))
		{
			var mapData = Files.SafeLoadResource<MapData>(path);
			if (mapData.GroundLayerData != null) GroundLayer.Load(mapData.GroundLayerData);
			if (mapData.DecorationLayerData != null) DecorationLayer.Load(mapData.DecorationLayerData);
			Camera.Calibrate();
		} else
		{
		}
		
		State = MapState.Done;
	}
}
