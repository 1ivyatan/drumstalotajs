using Godot;
using Godot.Collections;
using System;
using System.Linq;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Battle;
using Drumstalotajs.Mapping;
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

public partial class InitDeviceAdjustment : Control
{
	[Export] private CircleSlider _azimuthSlider;
	[Export] private Label _azimuthLabel;
	[Export] private Wheel _angleSlider;
	[Export] private Label _angleLabel;
	
	private BattleScene _scene;
	private Map _map;
	
	public override void _Ready()
	{
		_scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
		_scene.BattleTopnav.Title = "Prepare devices";
		_azimuthSlider.ValueChanged += (double value) => {
			_azimuthLabel.Text = $"~{Math.Round(value)}°";
		};
		_angleSlider.ValueChanged += (double value) => {
			_angleLabel.Text = $"~{Math.Round(value)}°";
		};
	}
}
