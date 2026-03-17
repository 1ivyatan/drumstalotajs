using Godot;
using System;
using Godot.Collections;

namespace drumstalotajs.Resources.Maps;

[GlobalClass]
public partial class Meta : Resource
{
	[Export] public string Title { get; set; }
	[Export] public string MapPath { get; set; }
}
