using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Components;
using Drumstalotajs.Editor.Components;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Mapping.Cameras;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.LevelSelection.Components;
using Drumstalotajs.Resources.Levels;

namespace Drumstalotajs.LevelSelection;

public partial class LevelSelectionScene : Node2D
{
	[Export] private Button _return;
	[Export] private LevelMetaContainer LevelMetaContainer { get; set; }
	[Export] public Map Map { get; private set; }
	[Export] private Topnav Topnav { get; set; }
	public LevelSet LevelSet { get; private set; }
	
	public async override void _Ready()
	{
		LevelSet = Nodes.GetRoot().SaveManager.GetLevelSet("Rocky Island");
		Map.Selector.Filter = new SelectorFilter([Map.OverlayLayer]);
		Map.Mode = MapMode.HiddenInteractable;
		Topnav.Title = "Deploy";
		Map.Load(LevelSet.BackgroundMapPath);
		if (LevelSet != null)
		{
			foreach (var level in LevelSet.Levels)
			{
				var data = level.GetTileData();
				Map.OverlayLayer.AddTile(data);
			}
		}
		_return.Pressed += () => {
			Nodes.GetRoot().SceneManager.Start();
		};
	}
}
