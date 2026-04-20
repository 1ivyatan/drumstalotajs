using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class EntityLayerAtlasData : SceneLayerAtlasData
{
	[Export] public EntityType Type { get; private set; }
}
