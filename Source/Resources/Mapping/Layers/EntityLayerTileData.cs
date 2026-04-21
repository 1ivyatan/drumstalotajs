using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Resources.Mapping.Layers;

[GlobalClass]
public partial class EntityLayerTileData : SceneLayerTileData
{
	[Export] public double Azimuth { get; set; }
	[Export] public double Integrity { get; set; }
	[Export] public bool Player { get; set; }
}
