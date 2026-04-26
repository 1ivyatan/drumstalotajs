using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Components.Tiling;

public partial class PickedTileData : Resource
{
	public BaseLayer Layer { get; set; }
	public string Atlas { get; set; }
	public PickedTileData() {}
	public PickedTileData(BaseLayer layer, String atlas)
	{
		Layer = layer;
		Atlas = atlas;
	}
}
