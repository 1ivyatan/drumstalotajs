using Godot;
using System;

namespace Drumstalotajs.Resources.Mapping.FreeLayers;

[GlobalClass]
public partial class FreeLayerData : Resource
{
	[Export] public FreeTileData[] FreeTiles { get; set; }
}
