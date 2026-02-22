using Godot;
using System;

namespace Drumstalotajs.Resources.Levels
{
	[GlobalClass]
	public partial class Level : Resource
	{
		[ExportGroup("Meta")]
		[Export] public string Title { get; set; }
		[Export] public string Description { get; set; }
		[Export] public float Scale { get; set; }
		[Export] public float BaseHeight { get; set; }
		
		[ExportGroup("Devices")]
		[Export] public Godot.Collections.Dictionary<int, Levels.DeviceProps> Devices { get; set; }
		
		[ExportGroup("Patterns")]
		[Export] public TileMapPattern GroundPattern { get; set; }
		[Export] public TileMapPattern DecorationPattern { get; set; }
		[Export] public TileMapPattern EntityPattern { get; set; }
	}
}
