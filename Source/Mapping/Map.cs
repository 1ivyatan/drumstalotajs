using Godot;
using System;
using Drumstalotajs.Resources.Mapping;
using Drumstalotajs.Mapping.Overlays;

namespace Drumstalotajs.Mapping;

public partial class Map : Node2D
{
	[Export] private MapMeta MapMeta { get; set; }
	private MapData MapData { get; set; }
	public MapStatus Status { get; private set; } = MapStatus.Init;
	public OverlayLayer OverlayLayer { get; private set; }
	
	public override void _Ready()
	{
		OverlayLayer = GetNode("OverlayLayer") as OverlayLayer;
	}
	
	public void LoadMap(MapMeta mapMeta)
	{
		Status = MapStatus.Init;
		MapMeta = mapMeta;
		MapData = mapMeta.LoadMapData();
		OverlayLayer.Load(MapData.OverlayLayer);
		Status = MapStatus.Done;
	}
}
