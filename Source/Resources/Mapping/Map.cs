using Godot;
using System;
using Drumstalotajs.Scores;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class Map : Resource
{
	[ExportGroup("Topography")]
	[Export] public double BaseHeight { get; private set; }
	
	//[ExportGroup("Placables")]
	
	
	[ExportGroup("Scoring")]
	[Export] public double TimeLimit { get; private set; }
}
