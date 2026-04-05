using Godot;
using System;
using Drumstalotajs.Mapping;
using Drumstalotajs.Utils;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Mapping.Tiles.Overlays;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Resources.Progress;

namespace Drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	private Map Map { get; set; }
	
	public override void _Ready()
	{
		Map = GetNode("Map") as Map;
		Button exit = GetNode<Button>("Overlay/Topnav/Exit");
		Button export = GetNode<Button>("Overlay/Topnav/Export");
		
		exit.Pressed += () =>
		{
			Nodes.GetRoot().SceneManager.Start();
		};
		
		export.Pressed += () =>
		{
			Export();
		};
	}
	
	private void Export()
	{
		
	}
}
