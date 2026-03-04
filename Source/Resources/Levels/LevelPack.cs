using Godot;
using System;

namespace Drumstalotajs.Resources.Levels
{
	[GlobalClass]
	public partial class LevelPack : Resource
	{
		[ExportGroup("Meta")]
		[Export] public string Title { get; set; }
		
		[ExportGroup("Levels")]
		[Export] public Levels.LevelProps[] Levels { get; set; }
	}
}
