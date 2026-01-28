using Godot;
using System;

namespace Drumstalotajs.Resources
{
	[GlobalClass]
	public partial class Level : Resource
	{
		[Export]
		public string Title { get; set; }
		
		[Export]
		public string Description { get; set; }
		
		[Export]
		public float Scale { get; set; }
		
		[Export]
		public TileMapPattern GroundPattern { get; set; }
		
		[Export]
		public TileMapPattern EntityPattern { get; set; }
		
		public Level() : this("", "", 1.0f, null, null) {}
		
		public Level(string title, string description, float scale, TileMapPattern groundPattern, TileMapPattern entityPattern)
		{
			Title = title;
			Description = description;
			Scale = scale;
			GroundPattern = groundPattern ?? null;
			EntityPattern = entityPattern ?? null;
		}
	}
}
