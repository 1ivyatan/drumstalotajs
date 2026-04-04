using Godot;
using System;
using Drumstalotajs.Scores;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class Map : Resource
{
	[ExportGroup("Scoring")]
	[Export] public double TimeLimit { get; private set; }
	
	public Score PrepareScore()
	{
		return new Score(TimeLimit);
	}
}
