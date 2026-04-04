using Godot;
using System;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping.Selection;

namespace Drumstalotajs.Battle.Stages;

public partial class DevicePlacement : Control
{
	
	public override void _Ready()
	{
		BattleScene root = Nodes.GetSceneRoot();
		root.Map.Selector.Filter = new SelectorFilter([Map.OverlayLayer]);
	}
}
