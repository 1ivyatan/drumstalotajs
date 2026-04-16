using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Editor.Components;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Cameras;
using Drumstalotajs.Utils;

namespace Drumstalotajs.LevelSelection;

public partial class LevelSelectionScene : Node2D
{
	[Export] public Map Map { get; private set; }
	
	public override void _Ready()
	{
		Map.Mode = MapMode.Lock;
	}
}
