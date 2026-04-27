using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Editor.Components;

public partial class OverlayProps : Props
{
	[Export] private Map _map;
	[Export] private SpinBox _rotationSpinner;
	private OverlayTile _overlayTile = null;
	
	public override void Load(Tile tile)
	{
		if (tile is OverlayTile overlayTile)
		{
			_overlayTile = overlayTile;
			Visible = true;
		} else {
			Close();
			_overlayTile = null;
		}
	}
}
