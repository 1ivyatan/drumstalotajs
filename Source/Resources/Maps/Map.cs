using Godot;
using System;
using System.Linq;
using Godot.Collections;

namespace drumstalotajs.Resources.Maps;

[GlobalClass]
public partial class Map : Resource
{
	[ExportGroup("Layers")]
	[Export] public Layers.GroundLayer GroundLayer { get; set; }
	[Export] public Layers.DecorationLayer DecorationLayer { get; set; }
	[Export] public Layers.EntityLayer EntityLayer { get; set; }
	
	[ExportGroup("Placables")]
	[Export] public int MinPlacableEntities { get; set; } = 0;
	[Export] public int MaxPlacableEntities { get; set; }
	[Export] public Vector2[] PlacablePositions { get; set; }
	[Export] public Layers.Entities.PlacableEntityProperties[] PlacableEntities { get; set; }
	
	public Layers.Entities.PlacableEntityProperties GetPlacableEntityProperties(int id)
	{
		return PlacableEntities.First(ep => ep.Id == id);
	}
}
