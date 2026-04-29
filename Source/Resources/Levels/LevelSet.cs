using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Resources;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Resources.Levels;

[GlobalClass]
public partial class LevelSet : Resource
{
	[Export] public string Name { get; set; } = "";
	[Export(PropertyHint.File, "*.tres,*.res")] public string BackgroundMapPath { get; set; } = "";
	[Export] public LevelProps[] Levels { get; set; } = [];
}
