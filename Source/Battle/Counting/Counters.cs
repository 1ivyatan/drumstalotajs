using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Battle.Counting;

public partial class Counters : Node
{
	public TimeLeft TimeLeft { get; private set; }
	
	public override void _Ready()
	{
		TimeLeft = GetNode("TimeLeft") as TimeLeft;
	}
}
