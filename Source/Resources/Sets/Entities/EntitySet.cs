using Godot;
using System;
using System.Linq;
using Godot.Collections;

namespace drumstalotajs.Resources.Sets.Entities;

[GlobalClass]
public partial class EntitySet : Resource
{
	[Export] public int TileSize { get; set; }
	[Export] public Sets.Entities.EntityProperties[] Entities { get; set; }
	
	public Sets.Entities.EntityProperties GetEntityProps(int id)
	{
		return Entities.First(e => e.Id == id);
	}
}
