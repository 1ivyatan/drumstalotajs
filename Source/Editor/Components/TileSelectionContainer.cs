using Godot;
using System;
using Drumstalotajs.Editor;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs;

namespace Drumstalotajs.Editor.Components;

public partial class TileSelectionContainer : Control
{
	[Export] private Control _container;
	
	public void Load(LayerBase[] layers)
	{
		
	}
}
