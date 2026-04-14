using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class GroundLayerData : AtlasLayerData
{
	[Export] public AddedGroundHeightAtlas AddedHeights { get; private set; }
	public GroundLayerData(GroundLayer layer) : base(layer)
	{
		AddedHeights = layer.AddedHeights;
	}
}
