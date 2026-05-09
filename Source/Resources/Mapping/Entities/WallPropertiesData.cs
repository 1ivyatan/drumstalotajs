using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Resources.Mapping.Entities;

[GlobalClass]
public partial class WallPropertiesData : EntityPropertiesData
{
	[ExportGroup("Textures")]
	[Export] public Texture2D[] IntegrityStages { get; set; } = null;
}
