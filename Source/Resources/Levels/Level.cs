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
		
		[ExportGroup("Victory")]
		[Export] public Vector2[] MustDestroy { get; set; }
		
		[ExportGroup("Patterns")]
		[Export] public string GroundPatternPath { get; set; }
		[Export] public string DecorationPatternPath { get; set; }
		[Export] public string EntityPatternPath { get; set; }
	}
}
