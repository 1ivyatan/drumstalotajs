using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class GroundLayerData : AtlasLayerData
{
	[Export] public double BaseHeight { get; private set; }
	[Export] public AddedGroundHeightAtlas AddedHeights { get; private set; }
	public GroundLayerData(GroundLayer layer) : base(layer)
	{
		BaseHeight = layer.BaseHeight;
		AddedHeights = layer.AddedHeights;
	}
}
