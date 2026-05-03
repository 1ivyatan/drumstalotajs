using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Battle;

namespace Drumstalotajs.Battle.Stages;

public partial class DevicePlacement : Control
{
	public override void _Ready()
	{
		var sceneRoot = Nodes.GetSceneRoot();
		sceneRoot.BattleTopnav.Title = "Device placement";
	}
}
