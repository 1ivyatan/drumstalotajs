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
using Drumstalotajs.Battle.Stages;

namespace Drumstalotajs.Battle.Stages;

public partial class End : Control
{
	[Export] private Label _label;
	[Export] private Button _exit;
	[Export] private Button _retry;
	private BattleScene _scene;
	private Map _map;
	
	public override void _Ready()
	{
		_scene = Nodes.GetSceneRoot() as BattleScene;
		_map = _scene.Map;
		_map.Mode = MapMode.HiddenInteractable;
		_scene.BattleTopnav.Title = "Conclusion";
		
		_scene.ScoreManager.SetRunning(false);
		
		if (_scene.ScoreManager.HasVictory())
		{
			if (_scene.ScoreManager.IsInLevel())
			{
				_scene.ScoreManager.RecordScore();
			}
			_label.Text = "Successful operation!";
			_label.LabelSettings.FontColor = Colors.LimeGreen;
		} else
		{
			_label.Text = "Failiure!";
			_label.LabelSettings.FontColor = Colors.PaleVioletRed;
		}
		
		_exit.Pressed += () => {
			_scene.Exit();
		};
			
		_retry.Pressed += () => {
			_scene.Restart();
		};
	}
}
