using Godot;
using System;
using Drumstalotajs.Resources;
using Drumstalotajs.Resources.Sets.Layers.Overlays;

namespace Drumstalotajs.Mapping.Layers.Overlays;

public partial class OverlayLayer : FreeLayer
{
	[Export] private OverlayLayerSet OverlayLayerSet { get; set; }
}
