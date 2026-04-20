using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Entities;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Resources.Mapping.Layers;

[GlobalClass]
public partial class EntityLayerAtlasData : SceneLayerAtlasData
{
	[Export] public EntityType Type { get; private set; }
	//[Export] public EntityPropertiesData Properties { get; private set; }
}
