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

namespace Drumstalotajs.LevelSelection;

public partial class LevelSelectionScene : Node2D
{
	[Export] private Button _return;
	
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
