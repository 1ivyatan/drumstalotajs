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
	public MapMode Mode { get; set; } = MapMode.Lock;
	public MapState State { get; set; } = MapState.Empty;
	
	[Export] public GroundLayer GroundLayer { get; private set; }
	[Export] public Selector Selector { get; private set; }
	[Export] public Camera Camera { get; private set; }
	
	public void AddTile(LayerBase layer, string tileName, Vector2I position)
	{
		if (layer is GroundLayer && Types.Vector2I.ValidVector2I(tileName))
		{
			(layer as GroundLayer).AddTile(position, Types.Vector2I.StringToVector2I(tileName));
		}
	}
	
	public void RemoveTile(LayerBase layer, Vector2I position)
	{
		if (layer is GroundLayer)
		{
			(layer as GroundLayer).RemoveTile(position);
		}
	}
	
	public MapData Export()
	{
		return new MapData(this);
	}
}
