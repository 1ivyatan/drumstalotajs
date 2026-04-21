using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Resources;
using Drumstalotajs.Resources.Mapping.Layers;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class MapMeta : Resource
{
	[ExportGroup("Layers")]
	[Export] public string Name { get; set; } = "";
	[Export] public string Description { get; set; } = "";
	
	[ExportGroup("Paths")]
	[Export(PropertyHint.File, "*.tres")] public string MapResourcePath { get; set; } = "";
}
