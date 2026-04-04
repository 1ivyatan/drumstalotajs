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
	
	private Resources.Mapping.Map _mapData = null;
	private Resources.Mapping.MapMeta _mapMeta = null;
	
	public override void _Ready()
	{
		OverlayLayer = GetNode("OverlayLayer") as OverlayLayer;
		Selector = GetNode("Selector") as Selector;
	}
	
	public void Load(MapMeta mapMeta)
	{
		State = MapState.Loading;
		_mapMeta = mapMeta;
		_mapData = mapMeta.LoadMap();
		State = MapState.Done;
	}
	
	public Score PrepareScore()
	{
		if (_mapData != null)
		{
			return _mapData.PrepareScore();
		}
		return null;
	}
}
