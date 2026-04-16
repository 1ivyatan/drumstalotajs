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
	[Export] public Selector Selector { get; private set; }
	[Export] public Camera Camera { get; private set; }
	
	public override void _Ready()
	{
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
		if (layer is GroundLayer && Types.Vector2I.ValidVector2I(tileName))
		{
			(layer as GroundLayer).AddTile(position, Types.Vector2I.StringToVector2I(tileName));
			EmitEdit();
		}
	}
	
	public void RemoveTile(LayerBase layer, Vector2I position)
	{
		if (layer is GroundLayer)
		{
			(layer as GroundLayer).RemoveTile(position);
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
			GroundLayer.Load(mapData.GroundLayerData);
			Camera.Calibrate();
		} else
		{
		}
		
		State = MapState.Done;
	}
}
