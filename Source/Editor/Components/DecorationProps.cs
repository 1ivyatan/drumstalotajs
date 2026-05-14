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

public partial class DecorationProps : Props
{
	[Signal] public delegate void ClickedColorEventHandler(int id);
	
	[Export] private Map _map;
	[Export] private BaseButton _rotation0deg;
	[Export] private BaseButton _rotation90deg;
	[Export] private BaseButton _rotation180deg;
	[Export] private BaseButton _rotation270deg;
	[Export] public AtlasColorSwitcher ColorContainer;
	private AtlasTile _decorationTile = null;
	
	public override void _Ready()
	{
		ColorContainer.Load(_map.DecorationLayer);
		_rotation0deg.Pressed += () => { RotateTile(0); };
		_rotation90deg.Pressed += () => { RotateTile(90); };
		_rotation180deg.Pressed += () => { RotateTile(180); };
		_rotation270deg.Pressed += () => { RotateTile(270); };
		ColorContainer.ClickedColor += (int id) => { 
			ChangeTileSource(id);
		};
	}
	
	private async void ChangeTileSource(int id)
	{
		if (_decorationTile != null)
		{
			Vector2I position = _decorationTile.CellPosition;
			_map.DecorationLayer.ChangeTileSource(position, id);
			_decorationTile = _map.DecorationLayer.GetTile(position);
		}
	}
	
	private void RotateTile(double degrees)
	{
		if (_decorationTile != null)
		{
			_decorationTile.RotateTile(degrees);
		}
	}
	
	public override void Load(Tile tile)
	{
		if (tile is AtlasTile atlasTile)
		{
			_decorationTile = atlasTile;
			Visible = true;
		} else {
			Close();
			_decorationTile = null;
		}
	}
}
