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

namespace Drumstalotajs.LevelSelection;

public partial class LevelSelectionScene : Node2D
{
	[Export] private Button _return;
	[Export] private LevelMetaContainer LevelMetaContainer { get; set; }
	[Export] public Map Map { get; private set; }
	[Export] private Topnav Topnav { get; set; }
	
	public override void _Ready()
	{
		Map.Selector.Filter = new SelectorFilter([Map.OverlayLayer]);
		Map.Mode = MapMode.HiddenInteractable;
		Topnav.Title = "Deploy";
		_return.Pressed += () => {
			Nodes.GetRoot().SceneManager.Start();
		};
	}
}
