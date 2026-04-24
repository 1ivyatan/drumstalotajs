using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Editor.Components;

public partial class InsertWindow : Window
{
	[Export] private PackedScene _tileListScene;
	
	public override void _Ready()
	{
		
	}
	
	public void LoadTiles(BaseLayer[] layers)
	{
		foreach (var layer in layers)
	}
}
