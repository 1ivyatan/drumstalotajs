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
	
	public override void _Ready()
	{
		_rotationSpinner.ValueChanged += (double value) => {
			if (_overlayTile != null)
			{
				_overlayTile.RotationDegrees = (float)value;
			}
		};
	}
	
	public override void Load(Tile tile)
	{
		if (tile is OverlayTile overlayTile)
		{
			_overlayTile = overlayTile;
			_rotationSpinner.Value = overlayTile.RotationDegrees;
			Visible = true;
		} else {
			Close();
			_overlayTile = null;
		}
	}
}
