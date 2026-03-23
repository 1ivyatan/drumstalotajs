using Godot;
using System;
using Godot.Collections;

namespace drumstalotajs.Resources.Maps;

[GlobalClass]
public partial class Meta : Resource
{
	[Export] public string Title { get; set; }
	[Export] public string Desc { get; set; }
	[Export] public string MapPath { get; set; }
	
	public Map LoadMap()
	{
		return ResourceLoader.Load<Map>(MapPath);
	}
}
