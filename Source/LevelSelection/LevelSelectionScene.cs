using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
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
	[Export] private LevelMetaContainer LevelMetaContainer;
	
	public override void _Ready()
	{
		_return.Pressed += () => {
			Nodes.GetRoot().SceneManager.Start();
		};
	}

	public override void _Process(double delta)
	{
	}
}
