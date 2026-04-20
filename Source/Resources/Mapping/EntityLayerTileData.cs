using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class EntityLayerTileData : SceneLayerTileData
{
	[Export] public double Azimuth { get; set; }
}
