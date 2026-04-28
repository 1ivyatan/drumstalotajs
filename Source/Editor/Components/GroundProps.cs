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
	[Export] private AtlasColorSwitcher _colorContainer;
	
	private GroundTile _groundTile = null;
	
	public override void _Ready()
	{
		_colorContainer.Load(_map.GroundLayer);
		_colorContainer.ClickedColor += (int id) => { ChangeTileSource(id);};
		_addedHeightSpinner.ValueChanged += (double value) => {
			if (_groundTile != null)
			{
				_groundTile.SetAddedHeight(value);
			}
		};
	}
	
	private async void ChangeTileSource(int id)
	{
		if (_groundTile != null)
		{
			Vector2I position = _groundTile.CellPosition;
			_map.GroundLayer.ChangeTileSource(position, id);
			_groundTile = _map.GroundLayer.GetTile(position);
		}
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
