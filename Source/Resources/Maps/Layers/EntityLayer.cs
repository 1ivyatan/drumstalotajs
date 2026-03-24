using Godot;
using System;
using Godot.Collections;

namespace drumstalotajs.Resources.Maps.Layers;

[GlobalClass]
public partial class EntityLayer : Resource
{
	[ExportGroup("Entities")]
	[Export] public Dictionary<int, Godot.Collections.Array<Entities.EntityProperties>> Entities { get; set; }
	
	[ExportGroup("Placable")]
	[Export] public Vector2[] PlacablePositions { get; set; }
	[Export] public Entities.PlacableEntityProperties[] PlacableEntities { get; set; }
}
