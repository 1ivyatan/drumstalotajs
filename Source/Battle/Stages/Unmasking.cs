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
	[Export] private Button _call;
	[Export] private Button _skip;
	
	private BattleScene _scene;
	private Map _map;
	
	public override void _Ready()
	{
		_scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
		_map.Selector.Mode = SelectorMode.Locked;
		_scene.BattleTopnav.Title = "We need some help";
		_call.Pressed += () => {
			int randomNumber = Random.Shared.Next(1, 5);
			switch (randomNumber)
			{
				case 1: /* unmask and player fire */
					Nodes.GetRoot().ToastManager.SpawnOne("We uncovered, but enemy caught us! We fire first!");
					_map.OverlayLayer.ClearAllBlackTiles();
					_scene.StageManager.PlayerFiring();
					break;
				case 2: /* unmask and player adjustment */
					Nodes.GetRoot().ToastManager.SpawnOne("We uncovered silently, make adjustments.");
					_map.OverlayLayer.ClearAllBlackTiles();
					_scene.StageManager.DeviceAdjustment();
					break;
				case 3: /* unmask and enemy fire */
					Nodes.GetRoot().ToastManager.SpawnOne("We uncovered, but enemy caught us! Run for cover!");
					_map.OverlayLayer.ClearAllBlackTiles();
					_scene.StageManager.EnemyFiring();
					break;
				case 4: /* fail to unmask and enemy fire */
					Nodes.GetRoot().ToastManager.SpawnOne("Failed to uncover and enemy caught us! Run for cover!");
					_scene.StageManager.EnemyFiring();
					break;
				default: break;
			}
		};
		_skip.Pressed += () => {
			_scene.StageManager.PlayerFiring();
		};
	}
}
