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

public partial class GroundProps : Props
{
	[Export] private Map _map;
	[Export] private Label _baseRelHeight;
	[Export] private SpinBox _addedHeightSpinner;
	private GroundTile _groundTile = null;
	
	public override void _Ready()
	{
		_addedHeightSpinner.ValueChanged += (double value) => {
			if (_groundTile != null)
			{
				_groundTile.SetAddedHeight(value);
			}
		};
	}
	
	public override void Load(Tile tile)
	{
		if (tile is GroundTile groundTile)
		{
			_groundTile = groundTile;
			_baseRelHeight.Text = $"{_groundTile.GetBaseRelativeHeight()}+";
			_addedHeightSpinner.Value = _groundTile.GetAddedHeight();
			Visible = true;
		} else {
			Close();
			_groundTile = null;
		}
	}
}
