using Godot;
using System;

namespace Drumstalotajs.Resources
{
	[GlobalClass]
	public partial class Level : Resource
	{
		[ExportGroup("Meta")]
		[Export]
		public string Title { get; set; }
		
		[Export]
		public string Description { get; set; }
		
		[Export]
		public float Scale { get; set; }
		
		[Export]
		public float BaseHeight { get; set; }
		
		[ExportGroup("Devices")]
		[Export]							/* id, amount */
		public Godot.Collections.Dictionary<int, int> Devices { get; set; }
		
		[ExportGroup("Patterns")]
		[Export]
		public TileMapPattern GroundPattern { get; set; }
		
		[Export]
		public TileMapPattern DecorationPattern { get; set; }
		
		[Export]
		public TileMapPattern EntityPattern { get; set; }
		
		public Level() : this("", "", 1.0f, null, null, null, 0, null) {}
		
		public Level(string title, 
					 string description, 
					 float scale, 
					 TileMapPattern groundPattern, 
					 TileMapPattern decorationPattern, 
					 TileMapPattern entityPattern,
					 int baseHeight,
					 Godot.Collections.Dictionary<int, int> devices)
		{
			Title = title;
			Description = description;
			Scale = scale;
			GroundPattern = groundPattern ?? null;
			DecorationPattern = decorationPattern ?? null;
			EntityPattern = entityPattern ?? null;
			BaseHeight = baseHeight;
			Devices = devices ?? null;
		}
	}
}
