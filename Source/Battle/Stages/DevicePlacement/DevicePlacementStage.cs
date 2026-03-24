using Godot;
using System;

namespace drumstalotajs.Battle.Stages.DevicePlacement;

public partial class DevicePlacementStage : Stage
{
	public override void _Ready()
	{
		GD.Print(GetMap());
	}
}
