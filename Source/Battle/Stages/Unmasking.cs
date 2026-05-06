using Godot;
using Godot.Collections;
using System;
using System.Linq;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Battle;
using Drumstalotajs.Mapping;
using Drumstalotajs.Battle.Components;
using Drumstalotajs.Resources.Mapping.Sets;
using Drumstalotajs.Mapping.Entities;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Mapping.Overlays;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;
using System.Threading.Tasks;
using Drumstalotajs.Components;

namespace Drumstalotajs.Battle.Stages;

public partial class Unmasking : Control
{
	private BattleScene _scene;
	private Map _map;
	
	public override void _Ready()
	{
		_scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
		_map.Selector.Mode = SelectorMode.Locked;
		_scene.BattleTopnav.Title = "Uncover the unseeable";
	}
}
