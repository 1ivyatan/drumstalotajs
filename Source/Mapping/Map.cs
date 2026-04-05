using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Resources.Mapping;
using Drumstalotajs.Scores;

namespace Drumstalotajs.Mapping;

public partial class Map : Node2D
{
	public MapMode Mode { get; set; } = MapMode.Locked;
	public MapState State { get; private set; } = MapState.Empty;
	
	public OverlayLayer OverlayLayer { get; private set; }
	public Selector Selector { get; private set; }
	
	public Resources.Mapping.Map MapData { get; private set; } = null;
	public Resources.Mapping.MapMeta MapMeta { get; private set; } = null;
	
	public override void _Ready()
	{
		OverlayLayer = GetNode("OverlayLayer") as OverlayLayer;
		Selector = GetNode("Selector") as Selector;
	}
	
	public void Load(MapMeta mapMeta)
	{
		State = MapState.Loading;
		MapMeta = mapMeta;
		MapData = mapMeta.LoadMap();
		State = MapState.Done;
	}
}
