using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class EntityLayerData : SceneLayerData
{
	public EntityLayerData () {}
	public EntityLayerData (EntityLayer layer) : base(layer) {}
}
