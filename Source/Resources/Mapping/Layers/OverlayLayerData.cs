using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Resources.Mapping.Layers;

[GlobalClass]
public partial class OverlayLayerData : SceneLayerData
{
	public OverlayLayerData () {}
	public OverlayLayerData (OverlayLayer layer) : base(layer) {}
}
