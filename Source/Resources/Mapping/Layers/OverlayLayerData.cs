using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Resources.Mapping.Layers;

[GlobalClass]
public partial class OverlayLayerData : SceneLayerData
{
	public OverlayLayerData () {}
	public OverlayLayerData (OverlayLayer layer) : base(layer)
	{
		Tiles.Clear();
		foreach (OverlayTile instance in layer.Instances)
		{
			OverlayLayerTileData data = new OverlayLayerTileData(layer, instance);
			Tiles.Add(data);
		}
	}
}
