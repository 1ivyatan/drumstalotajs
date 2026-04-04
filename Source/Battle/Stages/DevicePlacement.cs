using Godot;
using System;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping.Selection;

namespace Drumstalotajs.Battle.Stages;

public partial class DevicePlacement : Control
{
	public override void _Ready()
	{
		var map = Nodes.GetSceneRoot().Map;
		BattleScene root = Nodes.GetSceneRoot();
		root.Map.Selector.Filter = new SelectorFilter([map.OverlayLayer]);
	}
}
