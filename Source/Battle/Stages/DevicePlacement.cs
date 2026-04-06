using Godot;
using System;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Selection;

namespace Drumstalotajs.Battle.Stages;

public partial class DevicePlacement : Control
{
	private Map _map;
	
	public override void _Ready()
	{
		BattleScene root = Nodes.GetSceneRoot();
		_map = root.Map;
		_map.Mode = MapMode.HiddenInteractable;
		_map.Selector.Filter = new SelectorFilter([_map.OverlayLayer]);
		root.Topnav.SetTitle("Device placement");
	}
}
