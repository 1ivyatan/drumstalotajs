using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class DecorationLayerData : AtlasLayerData
{
	public DecorationLayerData() : base() {}
	public DecorationLayerData(DecorationLayer layer) : base(layer)
	{
	}
}
