using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Cameras;

namespace Drumstalotajs.Mapping;

public partial class Map : Node2D
{
	public MapMode Mode { get; set; } = MapMode.Lock;
	public MapState State { get; set; } = MapState.Empty;
	[Export] public GroundLayer GroundLayer { get; private set; }
	[Export] public Camera Camera { get; private set; }
	
	public void AddTile(LayerBase layer, string tileName, Vector2I position)
	{
		if (layer is GroundLayer && Types.ValidVector2I(tileName))
		{
			(layer as GroundLayer).AddTile(position, Types.StringToVector2I(tileName));
		}
	}
	
	public void RemoveTile(LayerBase layer, Vector2I position)
	{
		if (layer is GroundLayer)
		{
			(layer as GroundLayer).RemoveTile(position);
		}
	}
}
