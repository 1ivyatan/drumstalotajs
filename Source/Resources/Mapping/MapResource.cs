using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Resources;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Resources.Mapping.Sets;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class MapResource : Resource
{
	[ExportGroup("Properties")]
	[Export] public Vector2I MetersPerCell { get; set; } = Constants.Mapping.TileSize;
	
	[ExportGroup("Devices")]
	[Export] public Dictionary<Vector2I, double> DevicePositions { get; set; } = new();
	[Export] public Array<DeviceProps> DeviceProps { get; set; } = new();
	[Export] public int MinTotalDevices { get; set; } = 1;
	[Export] public int MaxTotalDevices { get; set; } = 1;
	
	[ExportGroup("Targets")]
	[Export] public bool Counterbattery { get; set; } = false;
	
	[ExportGroup("Hardening")]
	[Export] public double TimeLimitSecs { get; set; } = 60;
	[Export] public bool PlayerResupply { get; set; } = true;
	[Export] public bool EnemyResupply { get; set; } = true;
	
	[ExportGroup("Layers")]
	[Export] public GroundLayerData GroundLayer { get; set; } = null;
	[Export] public AtlasLayerData DecorationLayer { get; set; } = null;
	[Export] public EntityLayerData EntityLayer { get; set; } = null;
	[Export] public OverlayLayerData OverlayLayer { get; set; } = null;
	
	public MapResource () {}
	public MapResource (Map map)
	{
		GroundLayer = map.GroundLayer.Export();
		DecorationLayer = map.DecorationLayer.Export();
		EntityLayer = map.EntityLayer.Export();
		OverlayLayer = map.OverlayLayer.Export();
	}
}
