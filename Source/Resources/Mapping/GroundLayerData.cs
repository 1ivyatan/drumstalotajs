using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class GroundLayerData : AtlasLayerData
{
	[Export] public double BaseHeight { get; set; }
	[Export] public AddedGroundHeightAtlas AddedHeights { get; set; }
	public GroundLayerData() : base() {}
	public GroundLayerData(GroundLayer layer) : base(layer)
	{
		BaseHeight = layer.BaseHeight;
		AddedHeights = layer.AddedHeights;
	}
}
