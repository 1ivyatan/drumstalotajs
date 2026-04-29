using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Resources;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Resources.Levels;

[GlobalClass]
public partial class LevelProps : Resource
{
	[Export] public string Name { get; set; } = "";
	[Export] public string Desc { get; set; } = "";
	[Export] public int Order { get; set; } = 0;
	[Export] public Vector2I InMapPosition { get; set; }
	[Export(PropertyHint.File, "*.tres,*.res")] public string MapPath { get; set; } = "";
}
