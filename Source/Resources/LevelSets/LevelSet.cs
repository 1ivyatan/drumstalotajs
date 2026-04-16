using Godot;
using System;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Resources.LevelSets;

[GlobalClass]
public partial class LevelSet : Resource
{
	[Export(PropertyHint.File, "*.tres")] public string BackgroundMapPath { get; set; }
}
