using Godot;
using System;

namespace drumstalotajs.Battle.Stages;

public partial class Stage : Node
{
	public Mapping.Map GetMap()
	{
		return GetNode<Node2D>("../../Map") as Mapping.Map;
	}
}
