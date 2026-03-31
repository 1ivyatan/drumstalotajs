using Godot;
using System;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class MapData : Resource
{
	[Export] public FreeLayers.FreeLayerData OverlayLayer { get; set; }
}
